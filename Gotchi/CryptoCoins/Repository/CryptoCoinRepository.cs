using Gotchi.Core.Repository;
using Gotchi.Portfolios.Models;
using Gotchi.Portfolios.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gotchi.CryptoCoins.Repository;

public class CryptoCoinRepository : RepositoryBase
{
    public CryptoCoinRepository(RepositorySettings repositorySettings)
        : base(repositorySettings)
    {
    }

    // insert instead !
    public bool Upsert(CryptoCoin model) => Upsert(model, model.Id);

    public bool DeleteAll() => Delete<Portfolio>();

    public CryptoCoin GetById(string id) => Get<CryptoCoin>(id);
    public CryptoCoin GetByCoinMarketId(string id) => Get<CryptoCoin>(id);
    public CryptoCoin GetByName(string id) => Get<CryptoCoin>(id);
    public CryptoCoin GetBySlug(string id) => Get<CryptoCoin>(id);
    public CryptoCoin GetBySymbol(string id) => Get<CryptoCoin>(id);


    public ICollection<CryptoCoin> GetAll() => GetAll<CryptoCoin>();

    public bool Exists(string id) => Exists<Portfolio>(id);

}
