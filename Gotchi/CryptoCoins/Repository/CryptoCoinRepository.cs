using Gotchi.Core.Repository;
using Gotchi.Portfolios.Models;
using MongoDB.Driver;

namespace Gotchi.CryptoCoins.Repository;

public class CryptoCoinRepository : RepositoryBase<CryptoCoin>, ICryptoCoinRepository
{
    public CryptoCoinRepository(CryptoCoinRepositorySettings repositorySettings)
        : base(repositorySettings)
    {
    }

    public void Insert(IList<CryptoCoin> models)
    {
        ConnectToMongo().InsertMany(models);
    }

    public bool DeleteAll() => DeleteAllEntries();
    public CryptoCoin GetByCoinMarketId(string coinMarketId) => base.GetByKeyStr("Id", coinMarketId);
    public CryptoCoin GetByName(string name) => base.GetByKeyStr("Name", name);
    public IEnumerable<CryptoCoin> GetBySlug(string slug) => base.GetManyByKeyStr("Slug", slug);
    public IEnumerable<CryptoCoin> GetBySymbol(string symbol) => base.GetManyByKeyStr("Symbol", symbol);
    public IEnumerable<CryptoCoin> GetAll() => base.GetAllEntries();

    public async Task<CryptoCoin?> GetByCoinMarketIdAsync(string coinMarketId) => await base.GetByKeyStrAsync("Id", coinMarketId);
    public async Task<CryptoCoin?> GetByNameAsync(string name) => await base.GetByKeyStrAsync("Name", name);
    public async Task<IEnumerable<CryptoCoin>> GetBySlugAsync(string slug) => await base.GetManyByKeyStrAsync("Slug", slug);
    public async Task<IEnumerable<CryptoCoin>> GetBySymbolAsync(string symbol) => await base.GetManyByKeyStrAsync("Symbol", symbol);

    public bool Exists(string coinMarketId) => base.EntryExists(coinMarketId);
    public bool HasAnyEntries() => base.EntriesAny();
    public CryptoCoin GetFirstEntry() => base.ConnectToMongo().FindSync(_ => true).FirstOrDefault();

}
