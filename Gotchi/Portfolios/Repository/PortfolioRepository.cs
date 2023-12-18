using Gotchi.Core.Repository;
using Gotchi.Portfolios.Models;

namespace Gotchi.Portfolios.Repository;
public class PortfolioRepository : RepositoryBase<Portfolio>, IPortfolioRepository
{
    public PortfolioRepository(PortfolioRepositorySettings repositorySettings)
        : base(repositorySettings)
    {
    }

    public bool Upsert(Portfolio model) => base.UpsertEntry(model);
    public bool Delete(Portfolio portfolio) => base.DeleteEntry(portfolio.Id);
    public bool Delete(string id) => base.DeleteEntry(id);
    public bool DeleteAll() => base.DeleteAllEntries();
    public Portfolio GetByPortfolioId(string portfolioId) => base.GetEntryById(portfolioId);
    public IEnumerable<Portfolio> GetByPersonId(string personId) => base.GetManyByKeyStr("AccountHolder.Id", personId);
    public IEnumerable<Portfolio> GetAll() => base.GetAllEntries();
    public bool Exists(string id) => base.EntryExists(id);

    #region Data Access
    public async Task<Portfolio?> GetByPersonIdAsync(string personId) => await base.GetByKeyStrAsync("AccountHolder.Id", personId);
    #endregion

}