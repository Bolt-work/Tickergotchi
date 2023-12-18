using Gotchi.Portfolios.Models;

namespace Gotchi.Portfolios.Repository;

public interface IPortfolioRepository
{
    bool Delete(Portfolio portfolio);
    bool Delete(string id);
    bool DeleteAll();
    bool Exists(string id);
    Portfolio GetByPortfolioId(string portfolioId);
    IEnumerable<Portfolio> GetAll();
    bool Upsert(Portfolio model);
    IEnumerable<Portfolio> GetByPersonId(string personId);
    Task<Portfolio?> GetByPersonIdAsync(string personId);
}