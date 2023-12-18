using Gotchi.Core.Models;
using Gotchi.Persons.DTOs;

namespace Gotchi.Portfolios.DTOs;
public class PortfolioDTO : CoreModelBase
{
    public PersonDTO AccountHolder { get; set; } = null!;
    public float Balance { get; set; }
    public DateTime BalanceLastUpdated { get; set; }
    public DateTime BalanceNextUpdated { get; set; }
    public IList<AssetDTO> Assets { get; set; } = null!;
}
