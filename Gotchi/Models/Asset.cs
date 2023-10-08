using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gotchi.Models;

public class Asset : ModelBase
{
    public int CoinMarketId { get; set; }
    public string? Name { get; set; }
    public string? Slug { get; set; }
    public string? Symbol { get; set; }

    public float PriceWhenLastBought { get; set; }
    public float CurrentPrice { get; set; }
    public float Units { get; set; }
}
