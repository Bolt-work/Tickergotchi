using Gotchi.Models;

namespace Gotchi.Repository;

public interface IPersonRepository
{
    bool Delete(string id);
    bool Delete(Person id);
    bool DeleteAll();
    bool Exists(string id);
    ICollection<Person> Get();
    Person Get(string channelId);
    bool Upsert(Person model);
}