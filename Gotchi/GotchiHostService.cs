using AutoMapper;
using Gotchi.Core.Services;
using Gotchi.CryptoCoins;
using Gotchi.CryptoCoins.DTOs;
using Gotchi.Gotchis;
using Gotchi.Persons;
using Gotchi.Persons.DTOs;
using Gotchi.Persons.Models;
using Gotchi.Portfolios;
using Gotchi.Portfolios.DTOs;
using Gotchi.Portfolios.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gotchi;

public class GotchiHostService : CoreHostService
{
    private readonly ICoreCommandService _commandService;

    public GotchiHostService(string[] args) 
        : base(args)
    {
        _commandService = new CoreCommandService(_serviceProvider);
    }

    public ICoreCommandService CommandService() 
    {
        return _commandService;
    }

    public GotchiDataAccess DataAccess() 
    {
        return _serviceProvider.GetRequiredService<GotchiDataAccess>();
    }

    private IMapper BuildMapper() 
    {
        var config = new MapperConfiguration(cfg => 
        {
            // CryptoCoins
            cfg.CreateMap<CryptoCoin, CryptoCoinDTO>();

            // Persons
            cfg.CreateMap<Person,PersonDTO> ();

            // Portfolio
            cfg.CreateMap<Portfolio, PortfolioDTO> ();
            cfg.CreateMap<Asset, AssetDTO>();
        });
        
        return config.CreateMapper();
    }

    protected override IServiceCollection Registers(IServiceCollection services)
    {
        services.AddSingleton(BuildMapper());

        services.AddSingleton<GotchiDataAccess>();

        CryptoCoinServiceConfig.Register(services);
        PersonServiceConfig.Register(services);
        PortfolioServiceConfig.Register(services);
        GotchiServiceConfig.Register(services);

        return services;
    }
}
