using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gotchi.Models;

public class Portfolio : ModelBase
{
    public Person AccountHolder;
    public float Balance { get; set; }
    public IList<Asset> Assets;

    public Portfolio(string id, Person accountHolder)
    {
        Id = id;
        AccountHolder = accountHolder;
        Assets = new List<Asset>();
    }
}
