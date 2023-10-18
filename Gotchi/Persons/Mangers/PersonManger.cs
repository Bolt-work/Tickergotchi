using Gotchi.Core.Mangers;
using Gotchi.Persons.Models;
using Gotchi.Persons.Repository;

namespace Gotchi.Persons.Mangers
{
    public class PersonManger : CoreMangerBase, IPersonManger
    {
        private IPersonRepository _personRepository;

        public PersonManger(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public Person Create(string id, string? firstName = null, string? lastName = null)
        {
            if(_personRepository.Exists(id))
                throw new ModelWithIdAlreadyExistsException<Person>(id);

            return new Person(id) 
            {
                FirstName = firstName,
                LastName = lastName
            };
        }

        public Person GetById(string id)
        {
            var person = _personRepository.GetById(id);
            return ThrowIfModelNull(person, id);
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
