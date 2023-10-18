using Gotchi.Persons.Models;
using Gotchi.Persons.Repository;

namespace Gotchi.Tests.Mocks;

public class MockPersonRepository : MockRepositoryBase<Person>,  IPersonRepository
{
    public Person TestPerson;

    public MockPersonRepository()
    {
        TestPerson = new Person("testId")
        {
            FirstName = "testFirst",
            LastName = "testLast"
        };

        _db.Add(TestPerson);
    }
    public bool Delete(Person person) => base.Delete(person);
    public new bool Delete(string? id) => base.Delete(id);
    public new bool DeleteAll() => base.DeleteAll();
    public new bool Exists(string id) => base.Exists(id);
    public new ICollection<Person> GetAll() => base.GetAll();
    public new Person GetById(string personId) => base.GetById(personId);
    public new bool Upsert(Person model) => base.Upsert(model);
}
