using Gotchi.Gotchis.Models;
using Gotchi.Persons.Models;
using Gotchi.Portfolios.Models;

namespace Gotchi.Persons.Managers
{
    public interface IPersonManager
    {
        Task<bool> CheckPasswordAndPersonId(string? password, string? personId);
        Task<bool> CheckPasswordWithUserName(string? password, string? userName);
        Person ClearPersonActiveGotchi(Person person);
        Person ClearPersonActivePortfolio(Person person);
        Person Create(string? personId, string? userName, string? password);
        bool Delete(Person person);
        Task<bool> DoesUserNameAlreadyExistAsync(string? userName);
        IEnumerable<Person> GetAllPersons();
        Person GetPersonById(string? id);
        Task<Person?> GetPersonByIdAsync(string? personId);
        Person GetPersonByUserName(string? userName);
        Task<Person?> GetPersonByUserNameAsync(string? userName);
        Person SetPersonActiveGotchi(Person person, CryptoGotchi gotchi);
        Person SetPersonActivePortfolio(Person person, Portfolio portfolio);
        bool Store(Person person);
    }
}