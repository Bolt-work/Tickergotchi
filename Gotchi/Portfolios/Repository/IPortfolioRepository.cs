using Gotchi.Portfolios.Models;

namespace Gotchi.Portfolios.Repository;

public interface IPortfolioRepository
{
    bool Delete(Portfolio portfolio);
    bool Delete(string id);
    bool DeleteAll();
    bool Exists(string id);
    Portfolio Get(string id);
    ICollection<Portfolio> GetAll();
    bool Upsert(Portfolio model);
}