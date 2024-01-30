using Gotchi.Core.Repository;
using Gotchi.Portfolios.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Gotchi.CryptoCoins.Repository;

public class CryptoCoinRepository : RepositoryBase<CryptoCoin>, ICryptoCoinRepository
{
    public CryptoCoinRepository(CryptoCoinRepositorySettings repositorySettings)
        : base(repositorySettings)
    {
    }

    public void Insert(IEnumerable<CryptoCoin> models)
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

    public async Task<IEnumerable<string?>> GetNameSuggestionsAsync(string prefix, int limit = 20) 
    {
        var results = await GetSuggestionsAsync("Name", prefix, limit);
        return results.Select(x => x.Name).ToList();
    }

    public async Task<IEnumerable<string?>> GetSlugSuggestionsAsync(string prefix, int limit = 20)
    {
        var results = await GetSuggestionsAsync("Slug", prefix, limit, true);
        return results.Select(x => x.Slug).Distinct().ToList();
    }

    public async Task<IEnumerable<string?>> GetSymbolSuggestionsAsync(string prefix, int limit = 20)
    {
        var results = await GetSuggestionsAsync("Symbol", prefix, limit, true);
        return results.Select(x => x.Symbol).Distinct().ToList();
    }

    public async Task<IEnumerable<CryptoCoin>> GetSuggestionsAsync(string fieldName, string prefix, int limit = 20, bool distinct = false)
    {
        //var filter = Builders<CryptoCoin>.Filter.Regex("fieldName", new BsonRegularExpression(searchString, "i"));
        var filter = Builders<CryptoCoin>.Filter.Regex(fieldName, new BsonRegularExpression($"^{prefix}", "i"));
        var options = new FindOptions<CryptoCoin, CryptoCoin> { Limit = limit };

        return await ConnectToMongo().FindSync(filter, options).ToListAsync();
    }

}
