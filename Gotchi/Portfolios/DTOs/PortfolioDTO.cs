using Gotchi.Core.Models;
using Gotchi.Persons.DTOs;

namespace Gotchi.Portfolios.DTOs;
public class PortfolioDTO : ModelBase
{
    public PersonDTO AccountHolder;
    private float _balance;

    public float Balance
    {
        get
        {
            BalanceLastUpdated = DateTime.Now;
            return Utilities.CalculatePortfolioBalance(_balance, BalanceLastUpdated);
        }
        set { _balance = value; }
    }

    public DateTime BalanceLastUpdated { get; set; }
    public IList<AssetDTO> Assets;

    public PortfolioDTO(string id, PersonDTO accountHolder)
    {
        Id = id;
        AccountHolder = accountHolder;
        Assets = new List<AssetDTO>();
    }
}
