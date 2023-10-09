using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gotchi.Core.Models;

namespace Gotchi.Portfolios.Models;

public class CryptoCoin : ModelBase
{
    public int CoinMarketId { get; set; }
    public string? Name { get; set; }
    public string? Slug { get; set; }
    public string? Symbol { get; set; }

    public float Price { get; set; }

    public DateTime LastUpdated { get; set; }
    public DateTime CoinMarketLastUpdated { get; set; }
}
