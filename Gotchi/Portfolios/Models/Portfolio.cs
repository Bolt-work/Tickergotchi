using Gotchi.Core.Models;
using Gotchi.Persons.Models;

namespace Gotchi.Portfolios.Models;

public class Portfolio : ModelBase
{
    public Person AccountHolder;

    private float _balance;
    public float Balance
    {
        get {
            BalanceLastUpdated = DateTime.Now;
            return Utilities.CalculatePortfolioBalance(_balance, BalanceLastUpdated); 
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
