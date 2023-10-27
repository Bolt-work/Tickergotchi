using AutoMapper;
using Gotchi.Portfolios.DTOs;
using Gotchi.Portfolios.Managers;

namespace Gotchi.Portfolios.DataAccess;

internal class PortfolioDataAccess : IPortfolioDataAccess
{
    IPortfolioManager _portfolioManager;
    IMapper _mapper;
    public PortfolioDataAccess(IPortfolioManager portfolioManager, IMapper mapper)
    {
        _portfolioManager = portfolioManager;
        _mapper = mapper;
    }

    public PortfolioDTO PortfolioById(string portfolioId)
    {
        var portfolioModel = _portfolioManager.GetByPortfolioId(portfolioId);
        return _mapper.Map<PortfolioDTO>(portfolioModel);
    }

    public ICollection<PortfolioDTO> PortfoliosByPersonId(string personId)
    {
        var portfolioModels = _portfolioManager.GetByPersonId(personId);
        return _mapper.Map<ICollection<PortfolioDTO>>(portfolioModels);
    }

    public ICollection<PortfolioDTO> PortfoliosAll()
    {
        var portfolioModels = _portfolioManager.PortfolioAll();
        return _mapper.Map<ICollection<PortfolioDTO>>(portfolioModels);
    }
}
