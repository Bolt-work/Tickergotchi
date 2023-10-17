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

public class CryptoCoinRepository : RepositoryBase, ICryptoCoinRepository
{
    public CryptoCoinRepository(CryptoCoinRepositorySettings repositorySettings)
        : base(repositorySettings)
    {
    }

    public void Insert(IList<CryptoCoin> models)
    {
        ConnectToMongo<CryptoCoin>().InsertMany(models);
    }
    //public bool Upsert(CryptoCoin model) => Upsert(model, model.Id);

    public bool DeleteAll() => DeleteAllEntries<CryptoCoin>();

    public CryptoCoin GetByCoinMarketId(string coinMarketId) => base.GetByKeyStr<CryptoCoin>("Id", coinMarketId);

    public CryptoCoin GetByName(string name) => base.GetByKeyStr<CryptoCoin>("Name", name);
    public ICollection<CryptoCoin> GetBySlug(string slug) => base.GetManyByKeyStr<CryptoCoin>("Slug", slug);
    public ICollection<CryptoCoin> GetBySymbol(string symbol) => base.GetManyByKeyStr<CryptoCoin>("Symbol", symbol);
    public ICollection<CryptoCoin> GetAll() => base.GetAllEntries<CryptoCoin>();

    public bool Exists(string coinMarketId) => base.Exists<CryptoCoin>(coinMarketId);

}
