using Gotchi.Gotchis.Models;
using Gotchi.Persons.Models;

namespace Gotchi.Gotchis.Mangers
{
    public interface IGotchiManager
    {
        CryptoGotchi CreateCryptoGotchi(string id, Person owner);
    }
}