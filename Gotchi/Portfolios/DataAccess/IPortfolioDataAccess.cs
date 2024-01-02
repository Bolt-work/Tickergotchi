using Gotchi.Portfolios.DTOs;

namespace Gotchi.Portfolios.DataAccess;

public interface IPortfolioDataAccess
{
    PortfolioDTO PortfolioById(string portfolioId);
    Task<PortfolioDTO?> PortfolioByIdAsync(string portfolioId);
    Task<PortfolioDTO?> PortfolioByPersonIdAsync(string personId);
    ICollection<PortfolioDTO> PortfoliosAll();
    ICollection<PortfolioDTO> PortfoliosByPersonId(string personId);
}