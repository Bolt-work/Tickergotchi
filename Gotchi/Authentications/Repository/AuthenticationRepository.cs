using Gotchi.Authentications.Models;
using Gotchi.Core.Repository;

namespace Gotchi.Authentications.Repository;

public class AuthenticationRepository : RepositoryBase<AuthenticationModel>
{
    public AuthenticationRepository(AuthenticationRepositorySettings settings)
        : base(settings)
    {
    }

    public bool Upsert(AuthenticationModel model) => base.UpsertEntry(model);
    public bool Delete(string id) => base.DeleteEntry(id);
    public bool Delete(AuthenticationModel person) => base.DeleteEntry(person.Id);
    public bool DeleteAll() => base.DeleteAllEntries();
    public AuthenticationModel GetByAuthObjectId(string personId) => base.GetEntryById(personId);
    public bool ExistsByAuthId(string id) => base.EntryExists(id);
    public bool ExistsByAuthObjectId(string id) => base.EntryExists(id);
    public IEnumerable<AuthenticationModel> GetAll() => base.GetAllEntries();
}
