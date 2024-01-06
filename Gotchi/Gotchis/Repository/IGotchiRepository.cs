using Gotchi.Gotchis.Models;

namespace Gotchi.Gotchis.Repository
{
    public interface IGotchiRepository
    {
        bool Delete(CryptoGotchi gotchi);
        bool Delete(string id);
        bool DeleteAll();
        bool Exists(string id);
        CryptoGotchi GotchiByGotchiId(string id);
        IEnumerable<CryptoGotchi> GetAll();
        bool Upsert(CryptoGotchi gotchi);
        IEnumerable<CryptoGotchi> GotchisByOwnerId(string ownerId);
        Task<CryptoGotchi?> GotchiByGotchiIdAsync(string gotchiId);
    }
}