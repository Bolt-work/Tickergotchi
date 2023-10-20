using Gotchi.Core.Models;
using MongoDB.Driver;

namespace Gotchi.Tests.Mocks;

public abstract class MockRepositoryBase<T> where T : CoreModelBase
{
    public List<T> _db;

    protected MockRepositoryBase()
    {
        _db = new List<T>();
    }

    protected bool DeleteEntry(CoreModelBase model) => DeleteEntry(model.Id);

    protected bool DeleteEntry(string? id)
    {
        _db.RemoveAll(x => x.Id == id);
        return true;
    }

    protected bool DeleteAllEntries()
    {
        _db = new List<T>();
        return true;
    }

    protected bool EntryExists(string id)
    {
        return _db.Any(x => x.Id == id);
    }

    protected ICollection<T> GetAllEntries()
    {
        return _db;
    }

    protected T GetEntryById(string id)
    {
        return  _db.Single(x => x.Id == id);
    }

    protected bool InsertEntry(T model)
    {
        _db.Add(model);
        return true;
    }

    public void InsertEntries(IList<T> models) 
    {
        _db.AddRange(models);
    }

    protected bool UpsertEntry(T model)
    {
        DeleteEntry(model);
        InsertEntry(model);
        return true;
    }

}
