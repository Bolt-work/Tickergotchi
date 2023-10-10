using Gotchi.Core.Repository;
using Gotchi.Portfolios.Models;
using Gotchi.Portfolios.Repository;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gotchi.Gotchis.Repository;

public class GotchiRepository : RepositoryBase
{
    public GotchiRepository(RepositorySettings repositorySettings)
        : base(repositorySettings)
    {
    }

    public bool Upsert<T>(T model, string id) 
    {
        // attribute this !
            ConnectToMongo<T>().ReplaceOne(FilterId<T>(id), model, new ReplaceOptions { IsUpsert = true });
            return true;
    }

    public bool Delete(string id) => base.DeleteEntry<Portfolio>(id);
    public bool Delete(Portfolio portfolio) => base.DeleteEntry<Portfolio>(portfolio.Id);

    public bool DeleteAll() => base.DeleteAllEntries<Portfolio>();

    public Portfolio Get(string id) => base.GetEntry<Portfolio>(id);

    public ICollection<Portfolio> GetAll() => base.GetAllEntries<Portfolio>();

    public bool Exists(string id) => base.Exists<Portfolio>(id);

}
