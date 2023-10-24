
using Gotchi.Core.Helpers;
using Gotchi.Core.Mangers;
using Gotchi.Gotchis.Models;
using Gotchi.Gotchis.Repository;
using Gotchi.Persons.Models;
using Gotchi.Portfolios.Models;

namespace Gotchi.Gotchis.Mangers;

public class GotchiManager : IGotchiManager
{
    private IGotchiRepository _gotchiRepository;
    public GotchiManager(IGotchiRepository gotchiRepository)
    {
        _gotchiRepository = gotchiRepository;
    }

    public CryptoGotchi CreateCryptoGotchi(Person owner, string? gotchiId = null)
    {
        var id = gotchiId ?? CoreHelper.NewId();

        if (_gotchiRepository.Exists(id))
            throw new ModelWithIdAlreadyExistsException<Portfolio>(id);

        return new CryptoGotchi(id, owner) 
        {

        };
    }

    public bool Store(CryptoGotchi gotchi) 
    {
        return _gotchiRepository.Upsert(gotchi);
    }
}
