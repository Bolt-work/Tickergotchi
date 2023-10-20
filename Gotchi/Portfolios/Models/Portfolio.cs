using Gotchi.Core.Models;
using Gotchi.Persons.Models;

namespace Gotchi.Portfolios.Models;

public class Portfolio : CoreModelBase
{
    public Person AccountHolder;

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
    public IList<Asset> Assets;

    public Portfolio(string id, Person accountHolder)
    {
        Id = id;
        AccountHolder = accountHolder;
        Assets = new List<Asset>();
    }

}
