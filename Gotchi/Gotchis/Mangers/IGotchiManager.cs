using Gotchi.Gotchis.Models;
using Gotchi.Persons.Models;

namespace Gotchi.Gotchis.Managers
{
    public interface IGotchiManager
    {
        CryptoGotchi CreateCryptoGotchi(Person owner, string? name, string? gotchiId = null);
        void FeedGotchi(CryptoGotchi gotchi);
        CryptoGotchi GetGotchiById(string? gotchiId);
        IEnumerable<CryptoGotchi> GetGotchisByOwnerId(string? ownerId);
        bool Store(CryptoGotchi gotchi);
    }
}