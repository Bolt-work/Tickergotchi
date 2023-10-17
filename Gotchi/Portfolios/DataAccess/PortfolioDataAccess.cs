using AutoMapper;
using Gotchi.Core.Helpers;
using Gotchi.Persons.DataAccess;
using Gotchi.Persons.DTOs;
using Gotchi.Persons.Repository;
using Gotchi.Portfolios.DTOs;
using Gotchi.Portfolios.Models;
using Gotchi.Portfolios.Repository;

namespace Gotchi.Portfolios.DataAccess;

internal class PortfolioDataAccess : IPortfolioDataAccess
{
    IPortfolioRepository _portfolioRepository;
    IMapper _mapper;
    public PortfolioDataAccess(IPortfolioRepository portfolioRepository, IMapper mapper)
    {
        _portfolioRepository = portfolioRepository;
        _mapper = mapper;
    }

    public PortfolioDTO PortfolioById(string portfolioId)
    {
        var portfolioModel = _portfolioRepository.GetByPortfolioId(portfolioId);
        return _mapper.Map<PortfolioDTO>(portfolioModel);
    }

    public ICollection<PortfolioDTO> PortfoliosByPersonId(string personId)
    {
        var portfolioModels = _portfolioRepository.GetByPersonId(personId);
        return _mapper.Map<ICollection<PortfolioDTO>>(portfolioModels);
    }
}
