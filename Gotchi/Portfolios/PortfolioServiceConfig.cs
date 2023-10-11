using Gotchi.Gotchis.Mangers;
using Gotchi.Gotchis.Repository;
using Gotchi.Portfolios.Mangers;
using Gotchi.Portfolios.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace Gotchi.Portfolios;

public class PortfolioServiceConfig
{
    public static void Register(IServiceCollection services)
    {
        services.AddSingleton<PortfolioRepositorySettings>();
        services.AddSingleton<IPortfolioRepository, PortfolioRepository>();
        services.AddSingleton<IPortfolioManger, PortfolioManger>();

        // Command Handlers
        //services.AddSingleton<CreatePersonCommandHandler>();

    }
}
