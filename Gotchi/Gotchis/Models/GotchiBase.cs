using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gotchi.Core.Models;
using Gotchi.Persons.Models;

namespace Gotchi.Gotchis.Models;

public abstract class GotchiBase : ModelBase
{
    public Person Owner;
    public string? Name { get; set; }
    public int Level { get; set; }
    public int Hunger { get; set; }
    public int HungerMax { get; set; }
    public int FoodUnitsConsumed { get; set; }
    public DateTime LastUpdated { get; set; }

    public GotchiBase(string id, Person owner)
    {
        Id = id;
        Owner = owner;
    }

}
