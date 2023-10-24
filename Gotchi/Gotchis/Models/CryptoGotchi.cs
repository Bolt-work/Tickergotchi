using Gotchi.Core.Models;
using Gotchi.Persons.Models;

namespace Gotchi.Gotchis.Models;

public class CryptoGotchi : CoreModelBase
{
    public Person Owner;
    public string? Name { get; set; }
    public int Level { get; set; }
    public int Hunger { get; set; }
    public int HungerMax { get; set; }
    public int FoodUnitsConsumed { get; set; }
    public DateTime LastUpdated { get; set; }

    public CryptoGotchi(string id, Person owner)
    {
        Id = id;
        Owner = owner;
    }
}