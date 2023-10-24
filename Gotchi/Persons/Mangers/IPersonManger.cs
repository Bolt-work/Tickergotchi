using Gotchi.Persons.Models;

namespace Gotchi.Persons.Mangers
{
    public interface IPersonManger
    {
        Person Create(string id, string? firstName = null, string? lastName = null);
        bool Delete(Person person);
        Person GetPersonById(string id);
        bool Store(Person person);
    }
}