
using Gotchi.Persons.Models;
using Gotchi.Persons.Repository;

namespace Gotchi
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var person = new Person(Guid.NewGuid().ToString());

            var personSettings = new PersonRepositorySettings();
            var personDb = new PersonRepository(personSettings);
            personDb.Upsert(person);


        }
    }
}