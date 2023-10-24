using Gotchi.Gotchis.Models;
using Gotchi.Persons.Models;

namespace Gotchi.Gotchis.Mangers
{
    public interface IGotchiManager
    {
        CryptoGotchi CreateCryptoGotchi(Person owner, string? gotchiId = null);
    }
}