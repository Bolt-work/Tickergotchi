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
            if (amountInValue > balance)
                throw new CannotAffordPurchaseOfAssetException(portfolio, coin, amountInValue);

            if (AssetAlreadyOwned(portfolio, coin))
            {
                var asset = RetrieveAsset(portfolio, coin);
                asset.MoneyInvested += amountInValue;
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

        private bool AssetAlreadyOwned(Portfolio portfolio, CryptoCoin coin) 
        {
            return portfolio.Assets.Any(x => x.CoinMarketId == coin.Id);
        }

        private Asset RetrieveAsset(Portfolio portfolio, CryptoCoin coin)
        {
            var asset = portfolio.Assets.Single(x => x.CoinMarketId == coin.Id);
            if(asset is null)
                throw new AssetNotFoundException(portfolio, coin);

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
                MoneyInvested = amountInValue,
            };
        }

        public bool Delete(Portfolio portfolio)
        {
            return _portfolioRepository.Delete(portfolio);
        }

        public bool Delete(string id)
        {
            return _portfolioRepository.Delete(id);
        }

        public void Update(Portfolio portfolio)
        {
        }

    }
}
