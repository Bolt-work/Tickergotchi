using Gotchi.CryptoCoins.Mangers;
using Gotchi.CryptoCoins.Repository;
using Gotchi.Gotchis.Mangers;
using Gotchi.Gotchis.Repository;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gotchi.Gotchis;

public class GotchiServiceConfig
{
    public static void Register(IServiceCollection services)
    {
        services.AddSingleton<GotchiRepositorySettings>();
        services.AddSingleton<IGotchiRepository, GotchiRepository>();
        services.AddSingleton<IGotchiManager, GotchiManager>();

        // Command Handlers
        //services.AddSingleton<CreatePersonCommandHandler>();

    }
}
