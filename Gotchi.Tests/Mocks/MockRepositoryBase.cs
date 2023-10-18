using Gotchi.Core.Models;
using MongoDB.Driver;

namespace Gotchi.Tests.Mocks;

public abstract class MockRepositoryBase<T> where T : ModelBase
{
    public List<T> _db;

    protected MockRepositoryBase()
    {
        _db = new List<T>();
    }

    protected bool Delete(ModelBase person) => Delete(person.Id);

    protected bool Delete(string? id)
    {
        _db.RemoveAll(x => x.Id == id);
        return true;
    }

    protected bool DeleteAll()
    {
        _db = new List<T>();
        return true;
    }

    protected bool Exists(string id)
    {
        return _db.Any(x => x.Id == id);
    }

    protected ICollection<T> GetAll()
    {
        return _db;
    }

    protected T GetById(string personId)
    {
        return  _db.Single(x => x.Id == personId);
    }

    protected bool Insert(T model)
    {
        _db.Add(model);
        return true;
    }

    public void Insert(IList<T> models) 
    {
        _db.AddRange(models);
    }

    protected bool Upsert(T model)
    {
        Delete(model);
        Insert(model);
        return true;
    }

}
