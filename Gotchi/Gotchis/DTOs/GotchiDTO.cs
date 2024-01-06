using Gotchi.Persons.DTOs;

namespace Gotchi.Gotchis.DTOs;

public class GotchiDTO
{
    public string? Id { get; set; }
    public PersonDTO Owner { get; set; } = null!;
    public string? Name { get; set; }
    public int Level { get; set; }
    public DateTime Created { get; set; }
    public int Hunger { get; set; }
    public int HungerMax { get; set; }
    public int FoodUnitsConsumed { get; set; }
    public DateTime LastUpdated { get; set; }
    public DateTime NextUpdated { get; set; }
    public GotchiStateDTO State { get; set; }
    public float PriceForFood { get; set; }
    public int LastHungerAmountPerHour { get; set; }


    public int NumberOfHearts(int totalDisplayNo) 
    {
        if (totalDisplayNo < 1)
            return 0;

        float perHeartValue = (float)HungerMax / (float)totalDisplayNo;
        float numberOfHearts = (float)Hunger / perHeartValue;
        
        if(numberOfHearts%1 > 0.5)
            return (int) Math.Ceiling(numberOfHearts);

        return (int)numberOfHearts;
    }

    public bool BelowLastHeart(int totalDisplayNo)
    {
        if (totalDisplayNo < 1)
            return true;

        float perHeartValue = (float)HungerMax / (float)totalDisplayNo;
        return Hunger < perHeartValue;
    }

    public DateTime? EstimatedTimeOfDeath() 
    {
        if (State == GotchiStateDTO.Dead || Hunger <= 0)
            return null;

        float hours = Hunger / LastHungerAmountPerHour;
        int fullHours = (int)Math.Ceiling(hours);
        return LastUpdated.AddHours(fullHours);
    }

    public TimeSpan? EstimatedTimeTillDeath() 
    {
        if (State == GotchiStateDTO.Dead || Hunger <= 0)
            return null;

        var deathTime = EstimatedTimeOfDeath();
        if(deathTime is null)
            return null;

        return deathTime?.Subtract(DateTime.UtcNow);
    }

    public enum GotchiStateDTO : int
    {
        Dead = 0,
        Alive = 1,
    }
}
