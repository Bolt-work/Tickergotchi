using Gotchi.Persons.Models;

namespace Gotchi.Persons.Managers
{
    public interface IPersonManager
    {
        Person Create(string? personId, string? userName, string? password);
        Person Create(string? userName, string? password);
        bool Delete(Person person);
        IEnumerable<Person> GetAllPersons();
        Person GetPersonById(string? id);
        bool Store(Person person);
    }
}