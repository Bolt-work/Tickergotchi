using Gotchi.Core.Helpers;
using Gotchi.Gotchis.Models;
using Gotchi.Gotchis.Repository;

namespace Gotchi.Tests.Mocks;

public class MockGotchiRepository : MockRepositoryBase<CryptoGotchi>, IGotchiRepository
{
    public CryptoGotchi TestGotchi;
    public MockGotchiRepository(bool withTestData = false)
    {
        var owner = CommandHandlerHelper.MockPersonRepository().TestPerson;
        TestGotchi = new CryptoGotchi("testGotchiId", owner, "GotchiName")
        {
            Level = 0,
            Hunger = GameSettings.Values().StartingMaxHunger,
            HungerMax = GameSettings.Values().StartingMaxHunger,
            FoodUnitsConsumed = 0,
            LastUpdated = DateTime.Now,
            State = GotchiState.Alive,
            PriceForFood = GameSettings.Values().FoodBaseCost
        };

        if (withTestData)
            AddTestGotchi();
    }

    public void AddTestGotchi()
    {
        _db.Add(TestGotchi);
    }

    public bool Delete(CryptoGotchi gotchi) => base.DeleteEntry(gotchi);
    public bool Delete(string id) => base.DeleteEntry(id);
    public bool DeleteAll() => base.DeleteAllEntries();
    public bool Exists(string id) => base.EntryExists(id);

    public CryptoGotchi Get(string id) => base.GetEntryById(id);
    public IEnumerable<CryptoGotchi> GetAll() => base.GetAllEntries();
    public bool Upsert(CryptoGotchi gotchi) => base.UpsertEntry(gotchi);
}
