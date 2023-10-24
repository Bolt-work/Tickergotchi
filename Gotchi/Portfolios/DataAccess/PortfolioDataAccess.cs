using AutoMapper;
using Gotchi.Portfolios.DTOs;
using Gotchi.Portfolios.Mangers;

namespace Gotchi.Portfolios.DataAccess;

internal class PortfolioDataAccess : IPortfolioDataAccess
{
    IPortfolioManger _portfolioManger;
    IMapper _mapper;
    public PortfolioDataAccess(IPortfolioManger portfolioManger, IMapper mapper)
    {
        _portfolioManger = portfolioManger;
        _mapper = mapper;
    }

    public PortfolioDTO PortfolioById(string portfolioId)
    {
        var portfolioModel = _portfolioManger.GetByPortfolioId(portfolioId);
        return _mapper.Map<PortfolioDTO>(portfolioModel);
    }

    public ICollection<PortfolioDTO> PortfoliosByPersonId(string personId)
    {
        var portfolioModels = _portfolioManger.GetByPersonId(personId);
        return _mapper.Map<ICollection<PortfolioDTO>>(portfolioModels);
    }
}
