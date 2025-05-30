﻿using Gotchi.Core.Repository;
using Gotchi.Persons.Models;

namespace Gotchi.Persons.Repository;
public class PersonRepository : RepositoryBase<Person>, IPersonRepository
{
    public PersonRepository(PersonRepositorySettings repositorySettings)
        : base(repositorySettings)
    {
    }

    public bool Upsert(Person model) => base.UpsertEntry(model);
    public bool Delete(string id) => base.DeleteEntry(id);
    public bool Delete(Person person) => base.DeleteEntry(person.Id);
    public bool DeleteAll() => base.DeleteAllEntries();
    public Person GetById(string personId) => base.GetEntryById(personId);
    public bool ExistsById(string id) => base.EntryExists(id);
    public IEnumerable<Person> GetAll() => base.GetAllEntries();

    #region Data Access
    public async Task<Person?> GetByIdAsync(string personId) => await base.GetEntryByIdAsync(personId);
    #endregion
}