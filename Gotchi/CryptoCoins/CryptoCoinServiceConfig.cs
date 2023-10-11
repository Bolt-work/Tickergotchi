using Gotchi.CryptoCoins.Mangers;
using Gotchi.CryptoCoins.Repository;
using Gotchi.Persons.CommandServices;
using Gotchi.Persons.Mangers;
using Gotchi.Persons.Repository;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gotchi.CryptoCoins;

public class CryptoCoinServiceConfig
{
    public static void Register(IServiceCollection services)
    {
        services.AddSingleton<CryptoCoinRepositorySettings>();
        services.AddSingleton<ICryptoCoinRepository, CryptoCoinRepository>();
        services.AddSingleton<ICryptoCoinManger, CryptoCoinManger>();

        // Command Handlers
        //services.AddSingleton<CreatePersonCommandHandler>();

    }
}
