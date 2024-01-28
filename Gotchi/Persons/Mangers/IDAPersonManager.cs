using Gotchi.Persons.Models;

namespace Gotchi.Persons.Mangers;

public interface IDAPersonManager
{
    Task<Person?> GetPersonByIdAsync(string? personId);
}
