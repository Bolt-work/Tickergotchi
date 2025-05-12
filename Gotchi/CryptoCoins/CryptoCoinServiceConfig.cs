using CoinMarketCap;
using Gotchi.CryptoCoins.CommandServices;
using Gotchi.CryptoCoins.DataAccess;
using Gotchi.CryptoCoins.Managers;
using Gotchi.CryptoCoins.Mangers;
using Gotchi.CryptoCoins.Repository;
using Gotchi.Persons.CommandServices;
using Gotchi.Persons.Managers;
using Gotchi.Persons.Models;
using Gotchi.Persons.Repository;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Gotchi.CryptoCoins;

public class CryptoCoinServiceConfig
{
    public static void Register(IServiceCollection services)
    {
        // Command Handlers
        services.AddSingleton<UpdateCryptoCoinRepositoryCommandHandler>();

        // DataAccess
        services.AddSingleton<ICryptoCoinsDataAccess, CryptoCoinsDataAccess>();

        // Coin market Api
        string jsonString = File.ReadAllText("CoinMarketSecret.json");
        CoinMarketKeys coinMarketKeys = JsonSerializer.Deserialize<CoinMarketKeys>(jsonString);
        services.AddSingleton<CoinMarketKeys>(coinMarketKeys);
        services.AddSingleton<ICoinMarketApi, CoinMarketApi>();

        // Repository
        services.AddSingleton<CryptoCoinRepositorySettings>();
        services.AddSingleton<ICryptoCoinRepository, CryptoCoinRepository>();
        services.AddSingleton<ICryptoCoinManager, CryptoCoinManager>();
        services.AddSingleton<IDACryptoCoinManager, CryptoCoinManager>();
    }
}
