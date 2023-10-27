using Gotchi.Portfolios.DTOs;

namespace Gotchi.Portfolios.DataAccess;

public interface IPortfolioDataAccess
{
    PortfolioDTO PortfolioById(string portfolioId);
    ICollection<PortfolioDTO> PortfoliosAll();
    ICollection<PortfolioDTO> PortfoliosByPersonId(string personId);
}