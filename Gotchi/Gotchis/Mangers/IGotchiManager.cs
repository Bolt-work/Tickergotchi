using Gotchi.Gotchis.Models;
using Gotchi.Persons.Models;

namespace Gotchi.Gotchis.Managers
{
    public interface IGotchiManager
    {
        CryptoGotchi CreateCryptoGotchi(Person owner, string? name, string? gotchiId = null);
        void FeedGotchi(CryptoGotchi gotchi);
        CryptoGotchi GetGotchiById(string? gotchiId);
        Task<CryptoGotchi?> GetGotchiByIdAsync(string? gotchiId);
        IEnumerable<CryptoGotchi> GetGotchisByOwnerId(string? ownerId);
        IEnumerable<CryptoGotchi> GotchisAll();
        bool Store(CryptoGotchi gotchi);
    }
}