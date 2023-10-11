using Gotchi.Core.Repository;
using Gotchi.Gotchis.Models;
using Gotchi.Portfolios.Models;
using Gotchi.Portfolios.Repository;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using ZstdSharp;

namespace Gotchi.Gotchis.Repository;

public class GotchiRepository : RepositoryBase, IGotchiRepository
{
    public GotchiRepository(GotchiRepositorySettings repositorySettings)
        : base(repositorySettings)
    {
    }

    public bool Upsert(CryptoGotchi gotchi) => base.Upsert(gotchi);
    public bool Delete(string id) => base.DeleteEntry<CryptoGotchi>(id);
    public bool Delete(CryptoGotchi gotchi) => base.DeleteEntry<CryptoGotchi>(gotchi.Id);
    public bool DeleteAll() => base.DeleteAllEntries<CryptoGotchi>();
    public CryptoGotchi Get(string id) => base.GetEntry<CryptoGotchi>(id);
    public ICollection<CryptoGotchi> GetAll() => base.GetAllEntries<CryptoGotchi>();
    public bool Exists(string id) => base.Exists<CryptoGotchi>(id);

}
