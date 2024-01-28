using Gotchi.Portfolios.DTOs;

namespace Gotchi.Portfolios.DataAccess;

public interface IPortfolioDataAccess
{
    Task<PortfolioDTO?> PortfolioByIdAsync(string portfolioId);
    Task<PortfolioDTO?> PortfolioByPersonIdAsync(string personId);
}