
using Gotchi.Models;

namespace Gotchi.Repository;
public class PersonRepository : RepositoryBase, IPersonRepository
{
    public PersonRepository(RepositorySettings repositorySettings)
        : base(repositorySettings)
    {
    }

    public bool Upsert(Person model) => base.Upsert(model, model.Id);

    public bool Delete(string id) => base.Delete<Person>(id);
    public bool Delete(Person person) => Delete(person.Id);

    public bool DeleteAll() => base.Delete<Person>();

    public Person Get(string channelId) => base.Get<Person>(channelId);

    public ICollection<Person> Get() => base.Get<Person>();

    public bool Exists(string id) => base.Exists<Person>(id);

}