using Gotchi.CryptoCoins.Repository;
using Gotchi.Portfolios.Models;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Linq;

namespace Gotchi.Tests.Mocks;

public class MockCryptoCoinRepository : MockRepositoryBase<CryptoCoin>, ICryptoCoinRepository
{
    CryptoCoin TestCoin;
    public MockCryptoCoinRepository()
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

        _db.Add(TestCoin);
    }

    public new bool DeleteAll() => base.DeleteAll();
    public new bool Exists(string coinMarketId) => base.Exists(coinMarketId);
    public new ICollection<CryptoCoin> GetAll() => base.GetAll();

    public CryptoCoin GetByCoinMarketId(string coinMarketId) => base.GetById(coinMarketId);

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

    public new void Insert(IList<CryptoCoin> models) => base.Insert(models);
}
