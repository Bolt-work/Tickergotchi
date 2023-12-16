using Gotchi.Gotchis.Models;
using Gotchi.Persons.Models;
using Gotchi.Portfolios.Models;

namespace Gotchi.Persons.Managers
{
    public interface IPersonManager
    {
        bool CheckPasswordAndPersonId(string? password, string? personId);
        bool CheckPasswordWithUserName(string? password, string? userName);
        Person ClearPersonActiveGotchi(Person person);
        Person ClearPersonActivePortfolio(Person person);
        Person Create(string? personId, string? userName, string? password);
        bool Delete(Person person);
        bool DoesUserNameAlreadyExist(string? userName);
        IEnumerable<Person> GetAllPersons();
        Person GetPersonById(string? id);
        Person GetPersonByUserName(string? userName);
        Person SetPersonActiveGotchi(Person person, CryptoGotchi gotchi);
        Person SetPersonActivePortfolio(Person person, Portfolio portfolio);
        bool Store(Person person);
    }
}