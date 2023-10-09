using Gotchi.Persons.Models;
using Gotchi.Persons.Repository;

namespace Gotchi.Persons.Mangers
{
    public class PersonManger : IPersonManger
    {
        private IPersonRepository _personRepository;

        public PersonManger(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public Person Create(string id)
        {
            return new Person(id);
        }

        public Person Get(string id)
        {
            return _personRepository.Get(id);
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
