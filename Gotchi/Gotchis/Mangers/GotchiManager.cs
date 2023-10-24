
using Gotchi.Core.Helpers;
using Gotchi.Core.Managers;
using Gotchi.Gotchis.Models;
using Gotchi.Gotchis.Repository;
using Gotchi.Persons.Models;
using Gotchi.Portfolios.Models;
using System.Globalization;
using System.Reflection.Emit;
using System.Xml.Linq;

namespace Gotchi.Gotchis.Managers;

public class GotchiManager : IGotchiManager
{
    private IGotchiRepository _gotchiRepository;
    public GotchiManager(IGotchiRepository gotchiRepository)
    {
        _gotchiRepository = gotchiRepository;
    }

    public CryptoGotchi CreateCryptoGotchi(Person owner, string? name, string? gotchiId = null)
    {
        var id = gotchiId ?? CoreHelper.NewId();

        if (_gotchiRepository.Exists(id))
            throw new ModelWithIdAlreadyExistsException<Portfolio>(id);

        return BuildGotchi(owner, gotchiId, name);
    }

    private CryptoGotchi BuildGotchi(Person owner, string? gotchiId, string? name) 
    {
        return new CryptoGotchi(gotchiId, owner)
        {
            Id = gotchiId,
            //Name
            Level = 0,
            Hunger = GameSettings.Values().StartingMaxHunger,
            HungerMax = GameSettings.Values().StartingMaxHunger,
            FoodUnitsConsumed = 0,
            LastUpdated = DateTime.Now
        };
    }

    public bool Store(CryptoGotchi gotchi) 
    {
        return _gotchiRepository.Upsert(gotchi);
    }
}
