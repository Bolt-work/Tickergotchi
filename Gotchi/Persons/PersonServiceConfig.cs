using Gotchi.Persons.CommandServices;
using Gotchi.Persons.Mangers;
using Gotchi.Persons.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace Gotchi.Persons;

public class PersonServiceConfig
{
    public static void Register(IServiceCollection services)
    {
        services.AddSingleton<PersonRepositorySettings>();
        services.AddSingleton<IPersonRepository, PersonRepository>();
        services.AddSingleton<IPersonManger, PersonManger>();

        // Command Handlers
        services.AddSingleton<CreatePersonCommandHandler>();

    }
}
