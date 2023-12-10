using Gotchi.Persons.Models;

namespace Gotchi.Persons.Managers
{
    public interface IPersonManager
    {
        bool CheckPasswordAndPersonId(string? password, string? personId);
        bool CheckPasswordWithUserName(string? password, string? userName);
        Person Create(string? personId, string? userName, string? password);
        bool Delete(Person person);
        bool DoesUserNameAlreadyExist(string? userName);
        IEnumerable<Person> GetAllPersons();
        Person GetPersonById(string? id);
        Person GetPersonByUserName(string? userName);
        bool Store(Person person);
    }
}