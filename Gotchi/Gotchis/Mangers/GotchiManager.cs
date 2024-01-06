
using Gotchi.Core.Helpers;
using Gotchi.Core.Managers;
using Gotchi.Gotchis.Mangers;
using Gotchi.Gotchis.Models;
using Gotchi.Gotchis.Repository;
using Gotchi.Persons.Models;
using Gotchi.Portfolios.Managers;
using Gotchi.Portfolios.Models;
using Microsoft.Extensions.Logging;

namespace Gotchi.Gotchis.Managers;

public class GotchiManager : CoreManagerBase, IGotchiManager
{
    private IGotchiRepository _gotchiRepository;
    private ILogger _logger;
    public GotchiManager(IGotchiRepository gotchiRepository, ILogger<GotchiManager> logger)
    {
        _gotchiRepository = gotchiRepository;
        _logger = logger;
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

        var now = DateTime.UtcNow;
        return new CryptoGotchi(gotchiId, owner, name)
        {
            Level = 0,
            Created = DateTime.UtcNow,
            Hunger = GameSettings.Values().StartingMaxHunger,
            HungerMax = GameSettings.Values().StartingMaxHunger,
            FoodUnitsConsumed = 0,
            LastUpdated = now,
            NextUpdated = now.AddHours(1),
            State = GotchiState.Alive,
            PriceForFood = GameSettings.Values().FoodBaseCost,
            LastHungerAmountPerHour = GameSettings.Values().HungerAmountPerHour
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

    #region DataAccess
    public IEnumerable<CryptoGotchi> GetGotchisByOwnerIdAsync(string? ownerId)
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

    public async Task<CryptoGotchi?> GetGotchiByIdAsync(string? gotchiId)
    {
        if (gotchiId is null)
            return null;

        var gotchi = await _gotchiRepository.GotchiByGotchiIdAsync(gotchiId);

        if (gotchi is null)
            return null;

        UpdateWithTryCatch(gotchi);
        return gotchi;
    }
    #endregion

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

    public void UpdateWithTryCatch(CryptoGotchi gotchi) 
    {
        try 
        {
            Update(gotchi);
        }
        catch 
        (Exception ex) 
        {
            _logger.LogError("While updating Gotchi",ex);
        }
    }
    public void Update(CryptoGotchi gotchi) 
    {
        gotchi.PriceForFood = FindPriceOfFood(gotchi);
        gotchi.Level = CoreHelper.NumberOfDaysPassed(gotchi.Created);
        gotchi.LastHungerAmountPerHour = GameSettings.Values().HungerAmountPerHour;

        var timeNow = DateTime.UtcNow;
        var timePassed = timeNow - gotchi.LastUpdated;

        while (timePassed.TotalHours >= 1)
        {
            gotchi.LastUpdated = gotchi.LastUpdated.AddHours(1);
            gotchi.NextUpdated = gotchi.LastUpdated.AddHours(1);
            timePassed = timeNow - gotchi.LastUpdated;

            if (gotchi.State == GotchiState.Dead)
                break;

            
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
