
using Gotchi.Gotchis.Models;
using Gotchi.Gotchis.Repository;
using Gotchi.Persons.Models;

namespace Gotchi.Gotchis.Mangers;

public class GotchiManager : IGotchiManager
{
    private IGotchiRepository _gotchiRepository;
    public GotchiManager(IGotchiRepository gotchiRepository)
    {
        _gotchiRepository = gotchiRepository;
    }

    public CryptoGotchi CreateCryptoGotchi(string id, Person owner)
    {
        return new CryptoGotchi(id, owner);
    }
}
