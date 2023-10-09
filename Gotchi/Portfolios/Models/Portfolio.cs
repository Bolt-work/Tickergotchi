using Gotchi.Core.Models;
using Gotchi.Persons.Models;

namespace Gotchi.Portfolios.Models;

public class Portfolio : ModelBase
{
    public Person AccountHolder;
    public float Balance { get; set; }
    public DateTime BalanceLastUpdated { get; set; }
    public IList<Asset> Assets;

    public Portfolio(string id, Person accountHolder)
    {
        Id = id;
        AccountHolder = accountHolder;
        Assets = new List<Asset>();
    }
}
