using Gotchi.Core.Repository;
using Gotchi.Gotchis.Models;

namespace Gotchi.Gotchis.Repository;

public class GotchiRepository : RepositoryBase<CryptoGotchi>, IGotchiRepository
{
    public GotchiRepository(GotchiRepositorySettings repositorySettings)
        : base(repositorySettings)
    {
    }

    public bool Upsert(CryptoGotchi gotchi) => base.UpsertEntry(gotchi);
    public bool Delete(string id) => base.DeleteEntry(id);
    public bool Delete(CryptoGotchi gotchi) => base.DeleteEntry(gotchi.Id);
    public bool DeleteAll() => base.DeleteAllEntries();
    public CryptoGotchi Get(string id) => base.GetEntryById(id);
    public ICollection<CryptoGotchi> GetAll() => base.GetAllEntries();
    public bool Exists(string id) => base.EntryExists(id);

}
