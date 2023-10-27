using Gotchi.Gotchis.Models;
using Gotchi.Persons.DTOs;

namespace Gotchi.Gotchis.DTOs;

public class GotchiDTO
{
    public class CryptoGotchi
    {
        public string? Id { get; set; }
        public PersonDTO Owner { get; set; } = null!;
        public string? Name { get; set; }
        public int Level { get; set; }
        public int Hunger { get; set; }
        public int HungerMax { get; set; }
        public int FoodUnitsConsumed { get; set; }
        public DateTime LastUpdated { get; set; }
        public GotchiState State { get; set; }
        public float PriceForFood { get; set; }
    }
}
