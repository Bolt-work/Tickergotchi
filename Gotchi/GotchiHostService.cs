using AutoMapper;
using Gotchi.Core.Services;
using Gotchi.CryptoCoins;
using Gotchi.CryptoCoins.DTOs;
using Gotchi.Gotchis;
using Gotchi.Gotchis.DTOs;
using Gotchi.Gotchis.Models;
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
using static Gotchi.Gotchis.DTOs.GotchiDTO;

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

    public GotchiServiceDataAccess DataAccess() 
    {
        return _serviceProvider.GetRequiredService<GotchiServiceDataAccess>();
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

            // Gotchi
            cfg.CreateMap<CryptoGotchi, GotchiDTO>();
            cfg.CreateMap<GotchiState, GotchiStateDTO>();

        });
        
        return config.CreateMapper();
    }

    protected override IServiceCollection Registers(IServiceCollection services)
    {
        services.AddSingleton(BuildMapper());

        services.AddSingleton<GotchiServiceDataAccess>();

        CryptoCoinServiceConfig.Register(services);
        PersonServiceConfig.Register(services);
        PortfolioServiceConfig.Register(services);
        GotchiServiceConfig.Register(services);

        return services;
    }
}
