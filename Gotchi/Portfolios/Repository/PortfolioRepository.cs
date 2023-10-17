using Gotchi.Core.Repository;
using Gotchi.Portfolios.Models;

namespace Gotchi.Portfolios.Repository;
public class PortfolioRepository : RepositoryBase, IPortfolioRepository
{
    public PortfolioRepository(PortfolioRepositorySettings repositorySettings)
        : base(repositorySettings)
    {
    }

    public bool Upsert(Portfolio model) => base.Upsert(model);

    public bool Delete(Portfolio portfolio) => base.DeleteEntry<Portfolio>(portfolio.Id);
    public bool Delete(string id) => base.DeleteEntry<Portfolio>(id);

    public bool DeleteAll() => base.DeleteAllEntries<Portfolio>();

    public Portfolio GetByPortfolioId(string portfolioId) => base.GetEntry<Portfolio>(portfolioId);
    
    public ICollection<Portfolio> GetByPersonId(string personId) => base.GetManyByKeyStr<Portfolio>("AccountHolder.Id", personId);

    public ICollection<Portfolio> GetAll() => base.GetAllEntries<Portfolio>();

    public bool Exists(string id) => base.Exists<Portfolio>(id);

}