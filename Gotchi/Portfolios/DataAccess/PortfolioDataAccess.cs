using AutoMapper;
using Gotchi.Portfolios.DTOs;
using Gotchi.Portfolios.Managers;
using Gotchi.Portfolios.Mangers;

namespace Gotchi.Portfolios.DataAccess;

internal class PortfolioDataAccess : IPortfolioDataAccess
{
    IDAPortfolioManager _portfolioManager;
    IMapper _mapper;
    public PortfolioDataAccess(IDAPortfolioManager portfolioManager, IMapper mapper)
    {
        _portfolioManager = portfolioManager;
        _mapper = mapper;
    }

    public async Task<PortfolioDTO?> PortfolioByPersonIdAsync(string personId)
    {
        var portfolioModels = await _portfolioManager.GetByPersonIdAsync(personId);
        if(portfolioModels is null)
            return null;

        return _mapper.Map<PortfolioDTO>(portfolioModels);
    }

    public async Task<PortfolioDTO?> PortfolioByIdAsync(string portfolioId)
    {
        var portfolioModels = await _portfolioManager.GetByPortfolioIdAsync(portfolioId);
        if (portfolioModels is null)
            return null;

        return _mapper.Map<PortfolioDTO>(portfolioModels);
    }
}
