using Gotchi.Persons.Models;

namespace Gotchi.Persons.Repository;

public interface IPersonRepository
{
    bool Delete(string id);
    bool Delete(Person id);
    bool DeleteAll();
    bool Exists(string id);
    ICollection<Person> GetAll();
    Person Get(string personId);
    bool Upsert(Person model);
}