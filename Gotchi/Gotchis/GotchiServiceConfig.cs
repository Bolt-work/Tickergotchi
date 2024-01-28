using Gotchi.Gotchis.CommandServices;
using Gotchi.Gotchis.DataAccess;
using Gotchi.Gotchis.Managers;
using Gotchi.Gotchis.Mangers;
using Gotchi.Gotchis.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace Gotchi.Gotchis;

public class GotchiServiceConfig
{
    public static void Register(IServiceCollection services)
    {
        services.AddSingleton<GotchiRepositorySettings>();
        services.AddSingleton<IGotchiRepository, GotchiRepository>();
        services.AddSingleton<IGotchiManager, GotchiManager>();
        services.AddSingleton<IDAGotchiManager, GotchiManager>();

        // Data Access
        services.AddSingleton<IGotchiDataAccess, GotchiDataAccess>();

        // Command Handlers
        services.AddSingleton<FeedGotchiCommandHandler>();
        services.AddSingleton<CreateGotchiCommandHandler>();

    }
}
