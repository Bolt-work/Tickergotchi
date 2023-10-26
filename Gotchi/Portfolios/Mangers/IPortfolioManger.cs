using Gotchi.Persons.Models;
using Gotchi.Portfolios.Models;

namespace Gotchi.Portfolios.Managers
{
    public interface IPortfolioManager
    {
        void BuyAsset(Portfolio portfolio, CryptoCoin coin, float amount);
        Portfolio CreatePortfolio(Person accountHolder, string? portfolioId = null);
        bool DeletePortfolio(Portfolio portfolio);
        Portfolio GetByPortfolioId(string? portfolioId);
        IEnumerable<Portfolio> GetByPersonId(string personId);
        void RemovePortfolioAsset(Portfolio portfolio, string coinMarketId);
        void SellAsset(Portfolio portfolio, CryptoCoin coin, int units);
        bool Store(Portfolio portfolio);
        void Update(Portfolio portfolio);
        float WithdrawFromAccount(Portfolio portfolio, float amount);
    }
}