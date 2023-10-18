using Gotchi.Core.Repository;
using Gotchi.Portfolios.Models;
using Gotchi.Portfolios.Repository;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

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
    public ICollection<CryptoCoin> GetBySlug(string slug) => base.GetManyByKeyStr("Slug", slug);
    public ICollection<CryptoCoin> GetBySymbol(string symbol) => base.GetManyByKeyStr("Symbol", symbol);
    public ICollection<CryptoCoin> GetAll() => base.GetAllEntries();

    public bool Exists(string coinMarketId) => base.EntryExists(coinMarketId);

}
