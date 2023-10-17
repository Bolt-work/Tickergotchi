using Gotchi.Core.Repository;
using Gotchi.Persons.Models;

namespace Gotchi.Persons.Repository;
public class PersonRepository : RepositoryBase, IPersonRepository
{
    public PersonRepository(PersonRepositorySettings repositorySettings)
        : base(repositorySettings)
    {
    }

    public bool Upsert(Person model) => base.Upsert(model);

    public bool Delete(string id) => base.DeleteEntry<Person>(id);
    public bool Delete(Person person) => base.DeleteEntry<Person>(person.Id);
    public bool DeleteAll() => base.DeleteAllEntries<Person>();

    public Person GetById(string personId) => base.GetEntry<Person>(personId);
    public ICollection<Person> GetAll() => base.GetAllEntries<Person>();

    public bool Exists(string id) => base.Exists<Person>(id);

}