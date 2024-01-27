using Gotchi.Persons.Models;

namespace Gotchi.Persons.Repository;

public interface IPersonRepository
{
    bool Delete(string id);
    bool Delete(Person person);
    bool DeleteAll();
    bool ExistsById(string id);
    IEnumerable<Person> GetAll();
    Person GetById(string personId);
    Task<Person?> GetByIdAsync(string personId);
    bool Upsert(Person model);
}