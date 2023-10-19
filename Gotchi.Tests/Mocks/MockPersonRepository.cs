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
            FirstName = "testFirst",
            LastName = "testLast"
        };

        if(addTestData)
            _db.Add(TestPerson);
    }

    public void AddTestPerson() => _db.Add(TestPerson);

    public bool Delete(Person person) => base.DeleteEntry(person);
    public bool Delete(string? id) => base.DeleteEntry(id);
    public bool DeleteAll() => base.DeleteAllEntries();
    public bool Exists(string id) => base.EntryExists(id);
    public ICollection<Person> GetAll() => base.GetAllEntries();
    public Person GetById(string personId) => base.GetEntryById(personId);
    public bool Upsert(Person model) => base.UpsertEntry(model);
}
