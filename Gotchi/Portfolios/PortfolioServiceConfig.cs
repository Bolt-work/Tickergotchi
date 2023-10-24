using Gotchi.Gotchis.Managers;
using Gotchi.Gotchis.Repository;
using Gotchi.Portfolios.CommandService;
using Gotchi.Portfolios.DataAccess;
using Gotchi.Portfolios.Managers;
using Gotchi.Portfolios.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace Gotchi.Portfolios;

public class PortfolioServiceConfig
{
    public static void Register(IServiceCollection services)
    {
        // CommandHandler
        services.AddSingleton<CreatePortfolioCommandHandler>();
        services.AddSingleton<DeletePortfolioCommandHandler>();
        services.AddSingleton<RemoveAssetCommandHandler>();
        services.AddSingleton<BuyAssetsCommandHandler>();
        services.AddSingleton<SellAssetsCommandHandler>();

        // DataAccess
        services.AddSingleton<IPortfolioDataAccess, PortfolioDataAccess>();

        // Repository
        services.AddSingleton<PortfolioRepositorySettings>();
        services.AddSingleton<IPortfolioRepository, PortfolioRepository>();
        services.AddSingleton<IPortfolioManager, PortfolioManager>();

    }
}
