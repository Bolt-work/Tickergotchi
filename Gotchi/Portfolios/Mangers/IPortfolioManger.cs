using Gotchi.Persons.Models;
using Gotchi.Portfolios.Models;

namespace Gotchi.Portfolios.Mangers
{
    public interface IPortfolioManger
    {
        void BuyAsset(Portfolio portfolio, CryptoCoin coin, float amount);
        Portfolio CreatePortfolio(Person accountHolder, string? portfolioId = null);
        bool Delete(Portfolio portfolio);
        bool Delete(string id);
        Portfolio GetByPortfolioId(string id);
        bool Store(Portfolio portfolio);
        void Update(Portfolio portfolio);
    }
}