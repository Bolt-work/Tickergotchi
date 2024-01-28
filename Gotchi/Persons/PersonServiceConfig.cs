using Gotchi.Persons.CommandServices;
using Gotchi.Persons.DataAccess;
using Gotchi.Persons.Managers;
using Gotchi.Persons.Mangers;
using Gotchi.Persons.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace Gotchi.Persons;

public class PersonServiceConfig
{
    public static void Register(IServiceCollection services)
    {
        // Command Handlers
        services.AddSingleton<CreatePersonCommandHandler>();

        // Data Access
        services.AddSingleton<IPersonDataAccess, PersonDataAccess>();

        // Repository
        services.AddSingleton<PersonRepositorySettings>();
        services.AddSingleton<IPersonRepository, PersonRepository>();
        services.AddSingleton<IPersonManager, PersonManager>();
        services.AddSingleton<IDAPersonManager, PersonManager>();
    }
}
