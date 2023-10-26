
using Gotchi.Core.Helpers;
using Gotchi.Core.Managers;
using Gotchi.Gotchis.Mangers;
using Gotchi.Gotchis.Models;
using Gotchi.Gotchis.Repository;
using Gotchi.Persons.Models;
using Gotchi.Portfolios.Models;

namespace Gotchi.Gotchis.Managers;

public class GotchiManager : CoreManagerBase, IGotchiManager
{
    private IGotchiRepository _gotchiRepository;
    public GotchiManager(IGotchiRepository gotchiRepository)
    {
        _gotchiRepository = gotchiRepository;
    }

    public CryptoGotchi CreateCryptoGotchi(Person? owner, string? name, string? gotchiId = null)
    {
        var id = gotchiId ?? CoreHelper.NewId();

        if(owner is null)
            throw new ModelIsNullException<Person>();

        if (_gotchiRepository.Exists(id))
            throw new ModelWithIdAlreadyExistsException<CryptoGotchi>(id);

        return BuildGotchi(owner, gotchiId, name);
    }

    private CryptoGotchi BuildGotchi(Person owner, string? gotchiId, string? name) 
    {
        if(gotchiId is null)
            throw new ArgumentNullException(nameof(gotchiId));

        return new CryptoGotchi(gotchiId, owner, name)
        {
            Level = 0,
            Hunger = GameSettings.Values().StartingMaxHunger,
            HungerMax = GameSettings.Values().StartingMaxHunger,
            FoodUnitsConsumed = 0,
            LastUpdated = DateTime.Now,
            State = GotchiState.Alive,
            PriceForFood = GameSettings.Values().FoodBaseCost
        };
    }

    public CryptoGotchi GetGotchiById(string? gotchiId) 
    {
        if(gotchiId is null)
            throw new ArgumentNullException(nameof(gotchiId));

        var gotchi = _gotchiRepository.Get(gotchiId);
        ThrowIfModelNotFound(gotchi, gotchiId);
        Update(gotchi);
        return gotchi;
    }

    public bool Store(CryptoGotchi gotchi) 
    {
        return _gotchiRepository.Upsert(gotchi);
    }

    public void Update(CryptoGotchi gotchi) 
    {
        var hoursPassed = CoreHelper.NumberOfHoursPassed(gotchi.LastUpdated);
        gotchi.LastUpdated = DateTime.Now;
        
        for (int i = 0; i < hoursPassed; i++) 
        {
            if (gotchi.State == GotchiState.Dead)
                break;

            gotchi.Hunger -= GameSettings.Values().HungerAmountPerHour;

            if (gotchi.Hunger <= 0)
            {
                gotchi.State = GotchiState.Dead;
            }
        }

        gotchi.PriceForFood = FindPriceOfFood(gotchi);
    }

    private float FindPriceOfFood(CryptoGotchi gotchi) 
    {
        var price = GameSettings.Values().FoodBaseCost;
        return price + (gotchi.FoodUnitsConsumed * GameSettings.Values().FoodAddedCost);
    }

    public void FeedGotchi(CryptoGotchi gotchi)
    {
        Update(gotchi);
        CheckIfDead(gotchi);
        gotchi.Hunger += GameSettings.Values().FoodHungerValue;
        gotchi.FoodUnitsConsumed++;
        Update(gotchi);
    }

    private void CheckIfDead(CryptoGotchi gotchi) 
    { 
        if(gotchi.State == GotchiState.Dead)
            throw new GotchiIsDeadException(gotchi);
    }
}
