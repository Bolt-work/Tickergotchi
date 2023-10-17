using Gotchi.Gotchis.Mangers;
using Gotchi.Gotchis.Repository;
using Gotchi.Portfolios.CommandService;
using Gotchi.Portfolios.DataAccess;
using Gotchi.Portfolios.Mangers;
using Gotchi.Portfolios.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace Gotchi.Portfolios;

public class PortfolioServiceConfig
{
    public static void Register(IServiceCollection services)
    {
        // CommandHandler
        services.AddSingleton<CreatePortfolioCommandHandler>();
        services.AddSingleton<BuyAssetsCommandHandler>();

        // DataAccess
        services.AddSingleton<IPortfolioDataAccess, PortfolioDataAccess>();


        // Repository
        services.AddSingleton<PortfolioRepositorySettings>();
        services.AddSingleton<IPortfolioRepository, PortfolioRepository>();
        services.AddSingleton<IPortfolioManger, PortfolioManger>();

    }
}
