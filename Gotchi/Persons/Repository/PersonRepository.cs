using Gotchi.Core.Repository;
using Gotchi.Persons.Models;

namespace Gotchi.Persons.Repository;
public class PersonRepository : RepositoryBase, IPersonRepository
{
    public PersonRepository(RepositorySettings repositorySettings)
        : base(repositorySettings)
    {
    }

    public bool Upsert(Person model) => Upsert(model, model.Id);

    public bool Delete(string id) => Delete<Person>(id);
    public bool Delete(Person person) => Delete(person.Id);
    public bool DeleteAll() => Delete<Person>();
    public Person Get(string personId) => Get<Person>(personId);

    public ICollection<Person> GetAll() => GetAll<Person>();

    public bool Exists(string id) => Exists<Person>(id);

}