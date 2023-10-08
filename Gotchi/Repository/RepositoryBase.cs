using MongoDB.Bson;
using MongoDB.Driver;


namespace Gotchi.Repository;
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

    protected bool Upsert<T>(T model, string id)
    {
        ConnectToMongo<T>().ReplaceOne(FilterId<T>(id), model, new ReplaceOptions { IsUpsert = true });
        return true;
    }

    protected bool Delete<T>(string id)
    {
        ConnectToMongo<T>().DeleteOne(FilterId<T>(id));
        return true;
    }

    protected bool Delete<T>()
    {
        var filter = Builders<T>.Filter.Where( _ => true);
        ConnectToMongo<T>().DeleteMany(filter);
        return true;
    }

    protected T Get<T>(string id) 
    {
        return ConnectToMongo<T>().Find(FilterId<T>(id)).FirstOrDefault();
    }

    protected bool Exists<T>(string id)
    {
        return ConnectToMongo<T>().Find(FilterId<T>(id)).Any();
    }

    protected ICollection<T> Get<T>()
    {
        return ConnectToMongo<T>().Find(_ => true).ToList();
    }

    private FilterDefinition<T> FilterId<T>(string id) 
    {
        return Builders<T>.Filter.Eq("Id", id);
    }
}
