using Gotchi.CryptoCoins.Repository;
using Gotchi.Portfolios.Models;

namespace Gotchi.Tests.Mocks;

public class MockCryptoCoinRepository : MockRepositoryBase<CryptoCoin>, ICryptoCoinRepository
{
    CryptoCoin TestCoin;
    public MockCryptoCoinRepository(bool addTestData = false)
    {
        TestCoin = new CryptoCoin
        {
            Id = "1",
            Name = "Bitcoin",
            Symbol = "BTC",
            Slug = "bitcoin",
            Price = 27126.615234375F,
            CoinMarketLastUpdated = DateTime.UtcNow,
            LastUpdated = DateTime.UtcNow,
        };

        if (addTestData)
            AddTestCoin();
    }

    public void AddTestCoin() => _db.Add(TestCoin);


    public bool DeleteAll() => base.DeleteAllEntries();
    public bool Exists(string coinMarketId) => base.EntryExists(coinMarketId);
    public IEnumerable<CryptoCoin> GetAll() => base.GetAllEntries();

    public CryptoCoin GetByCoinMarketId(string coinMarketId) => base.GetEntryById(coinMarketId);

    public Task<CryptoCoin?> GetByCoinMarketIdAsync(string coinMarketId)
    {
        throw new NotImplementedException();
    }

    public CryptoCoin GetByName(string name)
    {
        return _db.SingleOrDefault(x => x.Name == name)!;
    }

    public Task<CryptoCoin?> GetByNameAsync(string name)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<CryptoCoin> GetBySlug(string slug)
    {
        return _db.Where(x => x.Slug == slug).ToList();
    }

    public Task<IEnumerable<CryptoCoin>> GetBySlugAsync(string slug)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<CryptoCoin> GetBySymbol(string symbol)
    {
        return _db.Where(x => x.Symbol == symbol).ToList();
    }

    public Task<IEnumerable<CryptoCoin>> GetBySymbolAsync(string symbol)
    {
        throw new NotImplementedException();
    }

    public CryptoCoin GetFirstEntry()
    {
        return _db.FirstOrDefault()!;
    }

    public bool HasAnyEntries()
    {
        return _db.Any(); 
    }

    public void Insert(IList<CryptoCoin> models) => base.InsertEntries(models);
}
