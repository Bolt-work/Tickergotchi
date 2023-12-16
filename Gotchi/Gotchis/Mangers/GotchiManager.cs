
using Gotchi.Core.Helpers;
using Gotchi.Core.Managers;
using Gotchi.Gotchis.Mangers;
using Gotchi.Gotchis.Models;
using Gotchi.Gotchis.Repository;
using Gotchi.Persons.Models;

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

        if (owner.ActiveGotchi is not null)
            throw new PersonAlreadyHasActiveGotchiException(owner);

        if (_gotchiRepository.Exists(id))
            throw new ModelWithIdAlreadyExistsException<CryptoGotchi>(id);

        return BuildGotchi(owner, id, name);
    }

    private CryptoGotchi BuildGotchi(Person owner, string? gotchiId, string? name) 
    {
        if(gotchiId is null)
            throw new ArgumentNullException(nameof(gotchiId));

        return new CryptoGotchi(gotchiId, owner, name)
        {
            Level = 0,
            Created = DateTime.UtcNow,
            Hunger = GameSettings.Values().StartingMaxHunger,
            HungerMax = GameSettings.Values().StartingMaxHunger,
            FoodUnitsConsumed = 0,
            LastUpdated = DateTime.UtcNow,
            State = GotchiState.Alive,
            PriceForFood = GameSettings.Values().FoodBaseCost
        };
    }

    public CryptoGotchi GetGotchiById(string? gotchiId)
    {
        if (gotchiId is null)
            throw new ArgumentNullException(nameof(gotchiId));

        var gotchi = _gotchiRepository.GotchiByGotchiId(gotchiId);
        ThrowIfModelNotFound(gotchi, gotchiId);
        Update(gotchi);
        return gotchi;
    }

    public IEnumerable<CryptoGotchi> GetGotchisByOwnerId(string? ownerId)
    {
        if (ownerId is null)
            throw new ArgumentNullException(nameof(ownerId));

        var gotchis = _gotchiRepository.GotchisByOwnerId(ownerId);
        foreach (var gotchi in gotchis) 
        {
            Update(gotchi);
        }

        return gotchis;
    }

    public IEnumerable<CryptoGotchi> GotchisAll()
    {
        var gotchis = _gotchiRepository.GetAll();
        Update(gotchis);
        return gotchis;
    }

    public bool Store(CryptoGotchi gotchi) 
    {
        return _gotchiRepository.Upsert(gotchi);
    }

    public void Update(IEnumerable<CryptoGotchi> gotchis) 
    {
        foreach (var gotchi in gotchis) 
        { 
            Update(gotchi); 
        }
    }

    public void Update(CryptoGotchi gotchi) 
    {
        gotchi.PriceForFood = FindPriceOfFood(gotchi);

        var minutesPassed = CoreHelper.NumberOfMinutesPassed(gotchi.LastUpdated);
        var iterations = minutesPassed / GameSettings.Values().UpdateGotchiInMinutes;

        for (int i = 0; i < iterations; i++) 
        {
            gotchi.LastUpdated = DateTime.UtcNow;

            if (gotchi.State == GotchiState.Dead)
                break;

            gotchi.Level = CoreHelper.NumberOfDaysPassed(gotchi.Created);

            gotchi.Hunger -= GameSettings.Values().HungerAmountPerHour;

            if (gotchi.Hunger <= 0)
            {
                gotchi.State = GotchiState.Dead;
            }
        }
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

        if (gotchi.Hunger == gotchi.HungerMax)
            throw new GotchiIsFullException(gotchi);

        gotchi.Hunger += GameSettings.Values().FoodHungerValue;
        gotchi.FoodUnitsConsumed++;

        if(gotchi.Hunger > gotchi.HungerMax)
            gotchi.Hunger = gotchi.HungerMax;
       
        Update(gotchi);
    }

    private void CheckIfDead(CryptoGotchi gotchi) 
    { 
        if(gotchi.State == GotchiState.Dead)
            throw new GotchiIsDeadException(gotchi);
    }
}
