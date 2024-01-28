using Gotchi.Portfolios.Models;

namespace Gotchi.Portfolios.Mangers;

public interface IDAPortfolioManager
{
    Task<Portfolio?> GetByPersonIdAsync(string? personId);
    Task<Portfolio?> GetByPortfolioIdAsync(string? portfolioId);
}
