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
            CoinMarketLastUpdated = DateTime.Now,
            LastUpdated = DateTime.Now,
        };

        if (addTestData)
            AddTestCoin();
    }

    public void AddTestCoin() => _db.Add(TestCoin);


    public bool DeleteAll() => base.DeleteAllEntries();
    public bool Exists(string coinMarketId) => base.EntryExists(coinMarketId);
    public ICollection<CryptoCoin> GetAll() => base.GetAllEntries();

    public CryptoCoin GetByCoinMarketId(string coinMarketId) => base.GetEntryById(coinMarketId);

    public CryptoCoin GetByName(string name)
    {
        return _db.SingleOrDefault(x => x.Name == name)!;
    }

    public ICollection<CryptoCoin> GetBySlug(string slug)
    {
        return _db.Where(x => x.Slug == slug).ToList();
    }

    public ICollection<CryptoCoin> GetBySymbol(string symbol)
    {
        return _db.Where(x => x.Symbol == symbol).ToList();
    }

    public void Insert(IList<CryptoCoin> models) => base.InsertEntries(models);
}
