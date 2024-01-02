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
    public CryptoGotchi GotchiByGotchiId(string gotchiId) => base.GetEntryById(gotchiId);
    public IEnumerable<CryptoGotchi> GotchisByOwnerId(string ownerId) => base.GetManyByKeyStr("Owner.Id", ownerId);
    public IEnumerable<CryptoGotchi> GetAll() => base.GetAllEntries();
    public bool Exists(string id) => base.EntryExists(id);

}
