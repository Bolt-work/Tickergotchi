using Gotchi.Models;
using Gotchi.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gotchi.Mangers
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
