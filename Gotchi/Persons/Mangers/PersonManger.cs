using Gotchi.Core.Managers;
using Gotchi.Persons.Models;
using Gotchi.Persons.Repository;

namespace Gotchi.Persons.Managers
{
    public class PersonManager : CoreManagerBase, IPersonManager
    {
        private IPersonRepository _personRepository;

        public PersonManager(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public Person Create(string id, string? firstName = null, string? lastName = null)
        {
            if(_personRepository.Exists(id))
                throw new ModelWithIdAlreadyExistsException<Person>(id);

            return new Person(id) 
            {
                FirstName = firstName ?? string.Empty,
                LastName = lastName ?? string.Empty,
            };
        }

        public Person GetPersonById(string? personId)
        {
            var id = personId ?? throw new ArgumentNullException(nameof(personId)); 
            var person = _personRepository.GetById(id);
            return ThrowIfModelNotFound(person, id);
        }

        public bool Delete(Person person)
        {
            return _personRepository.Delete(person);
        }

        public bool Store(Person person)
        {
            return _personRepository.Upsert(person);
        }

    }
}
