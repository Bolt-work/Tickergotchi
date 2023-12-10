using Gotchi.Persons.Models;
using Gotchi.Persons.Repository;

namespace Gotchi.Tests.Mocks;

public class MockPersonRepository : MockRepositoryBase<Person>,  IPersonRepository
{
    public Person TestPerson;

    public MockPersonRepository(bool addTestData = false)
    {
        TestPerson = new Person("testPersonId")
        {
            UserName = "testFirst",
            Password = "testLast"
        };

        if(addTestData)
            _db.Add(TestPerson);
    }

    public void AddTestPerson() => _db.Add(TestPerson);
    public bool Delete(Person person) => base.DeleteEntry(person);
    public bool Delete(string? id) => base.DeleteEntry(id);
    public bool DeleteAll() => base.DeleteAllEntries();
    public bool ExistsById(string id) => base.EntryExists(id);
    public bool ExistsByUserName(string userName) => _db.Any(x => x.UserName == userName);
    public IEnumerable<Person> GetAll() => base.GetAllEntries();
    public Person GetById(string personId) => base.GetEntryById(personId);

    public Person GetByUserName(string userName) => _db.First(x => x.UserName == userName);

    public bool Upsert(Person model) => base.UpsertEntry(model);
}
