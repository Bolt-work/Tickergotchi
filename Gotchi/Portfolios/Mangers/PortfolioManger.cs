using Gotchi.Core.Helpers;
using Gotchi.Core.Mangers;
using Gotchi.Persons.Models;
using Gotchi.Portfolios.Models;
using Gotchi.Portfolios.Repository;

namespace Gotchi.Portfolios.Mangers
{
    public class PortfolioManger : CoreMangerBase, IPortfolioManger
    {
        private IPortfolioRepository _portfolioRepository;

        public PortfolioManger(IPortfolioRepository portfolioRepository)
        {
            _portfolioRepository = portfolioRepository;
        }

        public Portfolio CreatePortfolio(Person accountHolder, string? portfolioId = null)
        {
            var id = portfolioId ?? CoreHelper.NewId();

            if (_portfolioRepository.Exists(id))
                throw new ModelWithIdAlreadyExistsException<Portfolio>(id);

            return new Portfolio(id, accountHolder) 
            {
                BalanceLastUpdated = DateTime.Now,
                Balance = GameSettings.Values().StartingBalance
            };
        }

        public bool Store(Portfolio portfolio)
        {
            return _portfolioRepository.Upsert(portfolio);
        }

        public Portfolio GetByPortfolioId (string id)
        {
            var portfolio = _portfolioRepository.GetByPortfolioId(id);
            ThrowIfModelNull(portfolio, id);
            Update(portfolio);
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
                asset.Units += amountInValue;
                asset.Profit -= amountInValue;
                asset.PriceWhenLastBought = coin.Price;
            }
            else 
            {
                var asset = BuildAsset(coin, amountInValue);
                portfolio.Assets.Add(asset);
            }

            portfolio.Balance = balance - amountInValue;
            portfolio.BalanceLastUpdated = DateTime.Now;
        }

        public void SellAsset(Portfolio portfolio, CryptoCoin coin, int units) 
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
            portfolio.BalanceLastUpdated = DateTime.Now;
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
                PriceWhenLastBought = coin.Price,
                Units = amountInValue,
                Profit = 0 - amountInValue,
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
        }

    }
}
