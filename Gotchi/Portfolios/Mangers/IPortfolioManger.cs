using Gotchi.Persons.Models;
using Gotchi.Portfolios.Models;

namespace Gotchi.Portfolios.Mangers
{
    public interface IPortfolioManger
    {
        Portfolio CreatePortfolio(Person person);
        Portfolio CreatePortfolio(string id, Person person);
        bool Delete(Portfolio portfolio);
        bool Delete(string id);
        Portfolio Get(string id);
        bool Store(Portfolio portfolio);
        void Update(Portfolio portfolio);
    }
}