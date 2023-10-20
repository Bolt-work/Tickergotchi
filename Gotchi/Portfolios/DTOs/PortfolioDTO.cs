using Gotchi.Core.Models;
using Gotchi.Persons.DTOs;

namespace Gotchi.Portfolios.DTOs;
public class PortfolioDTO : CoreModelBase
{
    public PersonDTO AccountHolder;
    private float _balance;

    public float Balance
    {
        get
        {
            _balance = PortfolioUtilities.CalculatePortfolioBalance(_balance, BalanceLastUpdated);
            BalanceLastUpdated = DateTime.Now;
            return _balance;
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
