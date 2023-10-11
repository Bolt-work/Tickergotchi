using Gotchi.Portfolios.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Xml.Linq;


namespace Gotchi.Core.Repository;
public abstract class RepositoryBase
{
    protected string _connectionString;
    protected string _databaseName;
    protected string _collectionName;

    public RepositoryBase(RepositorySettings repositorySettings)
    {
        _connectionString = repositorySettings.ConnectionString;
        _databaseName = repositorySettings.DatabaseName;
        _collectionName = repositorySettings.CollectionName;
    }

    protected IMongoCollection<T> ConnectToMongo<T>()
    {
        var client = new MongoClient(_connectionString);
        var db = client.GetDatabase(_databaseName);
        return db.GetCollection<T>(_collectionName);
    }

    protected string GetCoreId<T>(T model)
    {
        if (model is null)
            throw new ArgumentNullException("Mandatory parameter", nameof(model));
        
        foreach (var property in model.GetType().GetProperties())
        {
            foreach (var attribute in property.GetCustomAttributes(true))
            {
                if (attribute.GetType() == typeof(CoreId))
                {
                    var value = property.GetValue(model);
                    return Convert.ToString(value) ?? throw new NullReferenceException();
                }
            }
        }

        throw new CoreIdAttributeNotFoundException(model);
    }

    protected bool Upsert<T>(T model)
    {
        var id = GetCoreId<T>(model);
        ConnectToMongo<T>().ReplaceOne(FilterId<T>(id), model, new ReplaceOptions { IsUpsert = true });
        return true;
    }

    protected bool DeleteEntry<T>(string? id)
    {
        id = id ?? throw new ArgumentNullException(id);
        ConnectToMongo<T>().DeleteOne(FilterId<T>(id));
        return true;
    }

    protected bool DeleteAllEntries<T>()
    {
        var filter = Builders<T>.Filter.Where(_ => true);
        ConnectToMongo<T>().DeleteMany(filter);
        return true;
    }

    protected T GetEntry<T>(string id)
    {
        return ConnectToMongo<T>().Find(FilterId<T>(id)).FirstOrDefault();
    }

    protected bool Exists<T>(string id)
    {
        return ConnectToMongo<T>().Find(FilterId<T>(id)).Any();
    }

    protected ICollection<T> GetAllEntries<T>()
    {
        return ConnectToMongo<T>().Find(_ => true).ToList();
    }

    protected T GetByKeyStr<T>(string key, string value)
    {
        return ConnectToMongo<T>().Find<T>(Filter<T>(key, value)).SingleOrDefault();
    }

    protected ICollection<T> GetManyByKeyStr<T>(string key, string value)
    {
        return ConnectToMongo<T>().Find(Filter<T>(key, value)).ToList();
    }

    protected FilterDefinition<T> FilterId<T>(string id) => Filter<T>("Id", id);
    protected FilterDefinition<T> Filter<T>(string key, string id)
    {
        return Builders<T>.Filter.Eq(key, id);
    }
}
