using Gotchi.Persons.Models;
using Gotchi.Portfolios.Models;

namespace Gotchi.Portfolios.Mangers
{
    public interface IPortfolioManger
    {
        void BuyAsset(Portfolio portfolio, CryptoCoin coin, float amount);
        Portfolio CreatePortfolio(Person accountHolder, string? portfolioId = null);
        bool DeletePortfolio(Portfolio portfolio);
        Portfolio GetByPortfolioId(string id);
        IEnumerable<Portfolio> GetByPersonId(string personId);
        void RemovePortfolioAsset(Portfolio portfolio, string coinMarketId);
        void SellAsset(Portfolio portfolio, CryptoCoin coin, int units);
        bool Store(Portfolio portfolio);
        void Update(Portfolio portfolio);
    }
}