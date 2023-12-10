using Gotchi.Persons.Models;

namespace Gotchi.Persons.Repository;

public interface IPersonRepository
{
    bool Delete(string id);
    bool Delete(Person person);
    bool DeleteAll();
    bool ExistsById(string id);
    bool ExistsByUserName(string userName);
    IEnumerable<Person> GetAll();
    Person GetById(string personId);
    Person GetByUserName(string userName);
    bool Upsert(Person model);
}