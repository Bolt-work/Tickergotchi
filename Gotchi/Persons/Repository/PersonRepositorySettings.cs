using Gotchi.Core.Repository;

namespace Gotchi.Persons.Repository;

public class PersonRepositorySettings : RepositorySettings
{
    public PersonRepositorySettings()
    {
        CollectionName = "Persons";
    }
}
