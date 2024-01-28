using Gotchi.HighScores.CommandServices;
using Gotchi.HighScores.DataAccess;
using Gotchi.HighScores.Mangers;
using Gotchi.HighScores.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace Gotchi.HighScores;

public class HighScoreServiceConfig
{
    public static void Register(IServiceCollection services)
    {
        // CommandHandler
        services.AddSingleton<AddHighScoreCommandHandler>();

        // DataAccess
        services.AddSingleton<IHighScoreDataAccess, HighScoreDataAccess>();

        // Repository
        services.AddSingleton<HighScoreRepositorySettings>();
        services.AddSingleton<IHighScoreRepository, HighScoreRepository>();
        services.AddSingleton<IHighScoreManager, HighScoreManager>();
        services.AddSingleton<IDAHighScoreManager, HighScoreManager>();

    }
}
