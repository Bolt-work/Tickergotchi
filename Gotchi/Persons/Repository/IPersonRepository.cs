using Gotchi.Persons.Models;

namespace Gotchi.Persons.Repository;

public interface IPersonRepository
{
    bool Delete(string id);
    bool Delete(Person person);
    bool DeleteAll();
    bool Exists(string id);
    ICollection<Person> GetAll();
    Person GetById(string personId);
    bool Upsert(Person model);
}