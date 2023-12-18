using Gotchi.Core.Helpers;
using Gotchi.Core.Managers;
using Gotchi.CryptoCoins.Managers;
using Gotchi.Persons.Models;
using Gotchi.Portfolios.Models;
using Gotchi.Portfolios.Repository;
using Microsoft.Extensions.Logging;

namespace Gotchi.Portfolios.Managers
{
    public class PortfolioManager : CoreManagerBase, IPortfolioManager
    {
        private IPortfolioRepository _portfolioRepository;
        private ICryptoCoinManager _cryptoCoinManager;
        private ILogger _logger;

        public PortfolioManager(IPortfolioRepository portfolioRepository, ICryptoCoinManager cryptoCoinManager, ILogger<PortfolioManager> logger)
        {
            _portfolioRepository = portfolioRepository;
            _cryptoCoinManager = cryptoCoinManager;
            _logger = logger;
        }

        public Portfolio CreatePortfolio(Person accountHolder, string? portfolioId = null)
        {
            var id = portfolioId ?? CoreHelper.NewId();

            if (accountHolder.ActivePortfolio != null)
                throw new PersonAlreadyHasActivePortfolioException(accountHolder);

            if (_portfolioRepository.Exists(id))
                throw new ModelWithIdAlreadyExistsException<Portfolio>(id);

            var timeNow = DateTime.UtcNow;
            return new Portfolio(id, accountHolder) 
            {
                BalanceLastUpdated = timeNow,
                BalanceNextUpdated = timeNow.AddHours(1),
                Balance = GameSettings.Values().StartingBalance
            };
        }

        public bool Store(Portfolio portfolio)
        {
            return _portfolioRepository.Upsert(portfolio);
        }

        public Portfolio GetByPortfolioId (string? portfolioId)
        {
            if(portfolioId is null)
                throw new ArgumentNullException(nameof(portfolioId));

            var portfolio = _portfolioRepository.GetByPortfolioId(portfolioId);
            ThrowIfModelNotFound(portfolio, portfolioId);
            Update(portfolio);
            return portfolio;
        }

        public IEnumerable<Portfolio> GetByPersonId(string personId)
        {
            var portfolio = _portfolioRepository.GetByPersonId(personId);
            foreach (var p in portfolio) 
            { 
                Update(p); 
            }
            return portfolio;
        }

        public async Task<Portfolio?> GetByPersonIdAsync(string? personId) 
        {
            if (string.IsNullOrWhiteSpace(personId))
                return null;

            var portfolio = await _portfolioRepository.GetByPersonIdAsync(personId);
            if (portfolio is null)
                return null;

            try 
            {
                Update(portfolio);
            }
            catch (Exception ex) 
            {
                _logger.LogError("While updating portfolio",ex);
                return null;
            }

            return portfolio;
        }

        public IEnumerable<Portfolio> PortfolioAll()
        {
            var portfolio = _portfolioRepository.GetAll();
            foreach (var p in portfolio)
            {
                Update(p);
            }
            return portfolio;
        }

        public void BuyAsset(Portfolio portfolio, CryptoCoin coin, float amountInValue) 
        {
            // Encase it updates
            var balance = portfolio.Balance;
            if(amountInValue <= 0)
                throw new ArgumentOutOfRangeException(nameof(amountInValue));

            if (amountInValue > balance)
                throw new CannotAffordPurchaseOfAssetException(portfolio, coin, amountInValue);

            if (AssetOwned(portfolio, coin))
            {
                var asset = RetrieveAsset(portfolio, coin);
                asset.Units += (float) amountInValue / coin.Price;
                asset.Profit -= amountInValue;
                asset.PriceWhenLastBought = coin.Price;
            }
            else 
            {
                var asset = BuildAsset(coin, amountInValue);
                portfolio.Assets.Add(asset);
            }

            portfolio.Balance = balance - amountInValue;
            portfolio.BalanceLastUpdated = DateTime.UtcNow;
            portfolio.BalanceNextUpdated = portfolio.BalanceLastUpdated.AddHours(1);
        }

        public void SellAsset(Portfolio portfolio, CryptoCoin coin, float units) 
        {
            var asset = RetrieveAsset(portfolio, coin);

            if (units <= 0)
                throw new ArgumentOutOfRangeException(nameof(units));

            if (asset.Units < units)
                throw new AssetDoesHaveEnoughUnitsToSell(asset, units);

            var value = units * coin.Price;
            asset.Units -= units;
            asset.Profit += value;
            asset.PriceWhenLastBought = coin.Price;

            portfolio.Balance += value;
            portfolio.BalanceLastUpdated = DateTime.UtcNow;
            portfolio.BalanceNextUpdated = portfolio.BalanceLastUpdated.AddHours(1);
        }

        private bool AssetOwned(Portfolio portfolio, CryptoCoin coin) 
        {
            return portfolio.Assets.Any(x => x.CoinMarketId == coin.Id);
        }

        private Asset RetrieveAsset(Portfolio portfolio, CryptoCoin coin) 
        {
            var coinId = coin.Id ?? throw new ArgumentNullException(nameof(coin));
            return RetrieveAsset(portfolio, coinId);
        }

        private Asset RetrieveAsset(Portfolio portfolio, string coinMarketId)
        {
            var asset = portfolio.Assets.SingleOrDefault(x => x.CoinMarketId == coinMarketId);
            if(asset is null)
                throw new AssetNotFoundException(portfolio, coinMarketId);

            return asset;
        }

        private Asset BuildAsset(CryptoCoin coin, float amountInValue) 
        {
            return new Asset()
            {
                Id = CoreHelper.NewId(),
                CoinMarketId = coin.Id,
                Name = coin.Name,
                Slug = coin.Slug,
                Symbol = coin.Symbol,
                Units = (float) amountInValue / coin.Price,
                Profit = 0 - amountInValue,

                PriceWhenLastBought = coin.Price,
                CoinMarketLastUpdated = coin.CoinMarketLastUpdated,
                CoinLastUpdated = coin.LastUpdated,
                IsValid = true
            };
        }

        public void RemovePortfolioAsset(Portfolio portfolio, string coinMarketId)
        {
            var asset = RetrieveAsset(portfolio, coinMarketId);
            portfolio.Assets.Remove(asset);
        }

        public bool DeletePortfolio(Portfolio portfolio)
        {
            return _portfolioRepository.Delete(portfolio);
        }

        public void Update(Portfolio portfolio)
        {
            if (CoreHelper.NumberOfHoursPassed(portfolio.BalanceLastUpdated) > 0) 
            {
                portfolio.Balance = CalculatePortfolioBalance(portfolio.Balance, portfolio.BalanceLastUpdated);
                portfolio.BalanceLastUpdated = DateTime.UtcNow;
                portfolio.BalanceNextUpdated = portfolio.BalanceLastUpdated.AddHours(1);
            }

            foreach (var asset in portfolio.Assets) 
            {
                Update(asset);
            }
        }

        public static float CalculatePortfolioBalance(float balance, DateTime balanceLastUpdated) => CalculatePortfolioBalance(balance, balanceLastUpdated, DateTime.UtcNow);

        public static float CalculatePortfolioBalance(float balance, DateTime balanceLastUpdated, DateTime currentDataTime)
        {
            if (balance < 1)
                return 0;

            var hours = CoreHelper.NumberOfHoursPassed(balanceLastUpdated, currentDataTime);
            float newBalance = balance;
            for (int i = hours; i > 0; i--)
            {
                newBalance = newBalance - GameSettings.Values().DeductionBaseAmount;
                float deductionPercentage = (float)GameSettings.Values().DeductionPercentage / 100;
                newBalance -= (float)newBalance * deductionPercentage;
            }

            return (newBalance > 0) ? newBalance : 0;
        }

        public void Update(Asset asset)
        {
            if(asset.CoinMarketId is null)
                throw new ArgumentNullException(nameof(asset));

            var coin = _cryptoCoinManager.CryptoCoinByCoinMarketId(asset.CoinMarketId);

            if (coin is null)
            {
                asset.IsValid = false;
            }
            else 
            {
                asset.CurrentPrice = coin.Price;
                asset.CoinMarketLastUpdated = coin.CoinMarketLastUpdated;
                asset.CoinLastUpdated = coin.LastUpdated;
            }
        }

        public float WithdrawFromAccount(Portfolio portfolio, float amount) 
        {
            Update(portfolio);

            if (portfolio.Balance < amount)
                throw new NotEnoughFundsForWithdrawException(portfolio, amount);

            portfolio.Balance -= amount;
            return amount;
        }

    }
}
