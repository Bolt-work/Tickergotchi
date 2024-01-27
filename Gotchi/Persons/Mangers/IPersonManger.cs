using Gotchi.Gotchis.Models;
using Gotchi.Persons.Models;
using Gotchi.Portfolios.Models;

namespace Gotchi.Persons.Managers
{
    public interface IPersonManager
    {
        Person ClearPersonActiveGotchi(Person person);
        Person ClearPersonActivePortfolio(Person person);
        Person Create(string? personId);
        bool Delete(Person person);
        IEnumerable<Person> GetAllPersons();
        Person GetPersonById(string? id);
        Task<Person?> GetPersonByIdAsync(string? personId);
        Person SetPersonActiveGotchi(Person person, CryptoGotchi gotchi);
        Person SetPersonActivePortfolio(Person person, Portfolio portfolio);
        bool Store(Person person);
    }
}