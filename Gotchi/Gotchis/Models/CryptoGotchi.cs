using Gotchi.Core.Models;
using Gotchi.Persons.Models;

namespace Gotchi.Gotchis.Models;

public class CryptoGotchi : CoreModelBase
{
    public Person Owner;
    public string? Name { get; set; }
    public int Level { get; set; }
    public DateTime Created { get; set; }
    public int Hunger { get; set; }
    public int HungerMax { get; set; }
    public int FoodUnitsConsumed { get; set; }
    public DateTime LastUpdated { get; set; }
    public GotchiState State { get; set; }
    public float PriceForFood { get; set; }

    public CryptoGotchi(string id, Person owner, string? name)
    {
        Id = id;
        Owner = owner;
        Name = name;
    }
}

public enum GotchiState : int
{
    Dead = 0,
    Alive = 1,
}