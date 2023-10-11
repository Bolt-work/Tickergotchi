using Gotchi.Core.Helpers;
using Gotchi.Persons.Models;
using Gotchi.Portfolios.Models;
using Gotchi.Portfolios.Repository;

namespace Gotchi.Portfolios.Mangers
{
    public class PortfolioManger : IPortfolioManger
    {
        private IPortfolioRepository _portfolioRepository;

        public PortfolioManger(IPortfolioRepository portfolioRepository)
        {
            _portfolioRepository = portfolioRepository;
        }

        public Portfolio CreatePortfolio(Person person) => CreatePortfolio(CoreHelper.NewId(), person);
        public Portfolio CreatePortfolio(string id, Person person)
        {
            var portfolio = new Portfolio(id, person);
            portfolio.BalanceLastUpdated = DateTime.Now;
            // portfolio.Balance = json.starting balance
            return portfolio;
        }

        public bool Store(Portfolio portfolio)
        {
            return _portfolioRepository.Upsert(portfolio);
        }

        public Portfolio Get(string id)
        {
            return _portfolioRepository.Get(id);
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
            var hours = CoreHelper.NumberOfHoursPassed(portfolio.BalanceLastUpdated);
        }

    }
}
