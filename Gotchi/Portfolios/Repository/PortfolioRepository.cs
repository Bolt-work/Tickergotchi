using Gotchi.Core.Repository;
using Gotchi.Portfolios.Models;

namespace Gotchi.Portfolios.Repository;
public class PortfolioRepository : RepositoryBase, IPortfolioRepository
{
    public PortfolioRepository(RepositorySettings repositorySettings)
        : base(repositorySettings)
    {
    }

    public bool Upsert(Portfolio model) => Upsert(model, model.Id);

    public bool Delete(string id) => Delete<Portfolio>(id);
    public bool Delete(Portfolio portfolio) => Delete(portfolio.Id);

    public bool DeleteAll() => Delete<Portfolio>();

    public Portfolio Get(string id) => Get<Portfolio>(id);

    public ICollection<Portfolio> GetAll() => GetAll<Portfolio>();

    public bool Exists(string id) => Exists<Portfolio>(id);

}