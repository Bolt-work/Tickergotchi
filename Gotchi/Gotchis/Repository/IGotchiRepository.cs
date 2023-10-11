using Gotchi.Gotchis.Models;

namespace Gotchi.Gotchis.Repository
{
    public interface IGotchiRepository
    {
        bool Delete(CryptoGotchi gotchi);
        bool Delete(string id);
        bool DeleteAll();
        bool Exists(string id);
        CryptoGotchi Get(string id);
        ICollection<CryptoGotchi> GetAll();
        bool Upsert(CryptoGotchi gotchi);
    }
}