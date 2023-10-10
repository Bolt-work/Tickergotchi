using Gotchi.Core.Repository;
using Gotchi.Portfolios.Models;

namespace Gotchi.Portfolios.Repository;
public class PortfolioRepository : RepositoryBase, IPortfolioRepository
{
    public PortfolioRepository(RepositorySettings repositorySettings)
        : base(repositorySettings)
    {
    }

    public bool Upsert(Portfolio model) => base.Upsert(model);

    public bool Delete(Portfolio portfolio) => base.DeleteEntry<Portfolio>(portfolio.Id);
    public bool Delete(string id) => base.DeleteEntry<Portfolio>(id);

    public bool DeleteAll() => base.DeleteAllEntries<Portfolio>();

    public Portfolio Get(string id) => base.GetEntry<Portfolio>(id);

    public ICollection<Portfolio> GetAll() => base.GetAllEntries<Portfolio>();

    public bool Exists(string id) => base.Exists<Portfolio>(id);

}