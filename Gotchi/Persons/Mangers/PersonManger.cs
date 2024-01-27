using Gotchi.Core.Helpers;
using Gotchi.Core.Managers;
using Gotchi.Gotchis.Models;
using Gotchi.Persons.Models;
using Gotchi.Persons.Repository;
using Gotchi.Portfolios.Models;

namespace Gotchi.Persons.Managers
{
    public class PersonManager : CoreManagerBase, IPersonManager
    {
        private IPersonRepository _personRepository;

        public PersonManager(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public Person Create(string? personId)
        {
            if (string.IsNullOrEmpty(personId))
                personId = CoreHelper.NewId();

            if (_personRepository.ExistsById(personId))
                throw new ModelWithIdAlreadyExistsException<Person>(personId);

            return new Person(personId)
            {
                ActivePortfolio = null,
                ActiveGotchi = null,
            };
        }

        public Person SetPersonActivePortfolio(Person person, Portfolio portfolio)
        {
            person.ActivePortfolio = portfolio.Id;
            return person;
        }

        public Person ClearPersonActivePortfolio(Person person)
        {
            person.ActivePortfolio = null;
            return person;
        }

        public Person SetPersonActiveGotchi(Person person, CryptoGotchi gotchi)
        {
            person.ActiveGotchi = gotchi.Id;
            return person;
        }

        public Person ClearPersonActiveGotchi(Person person)
        {
            person.ActiveGotchi = null;
            return person;
        }

        public Person GetPersonById(string? personId)
        {
            if (string.IsNullOrEmpty(personId))
                throw new ArgumentNullException(nameof(personId));

            var person = _personRepository.GetById(personId);
            return ThrowIfModelNotFound(person, personId);
        }

        public IEnumerable<Person> GetAllPersons()
        {
            return _personRepository.GetAll();
        }

        public bool Delete(Person person)
        {
            return _personRepository.Delete(person);
        }

        public bool Store(Person person)
        {
            return _personRepository.Upsert(person);
        }

        #region Data Access

        public async Task<Person?> GetPersonByIdAsync(string? personId)
        {
            if (string.IsNullOrEmpty(personId))
                return null;

            return await _personRepository.GetByIdAsync(personId);
        }

        #endregion
    }
}
