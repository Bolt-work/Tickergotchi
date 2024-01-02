using Gotchi.Core.Models;

namespace Gotchi.Portfolios.Models;

public class CryptoCoin :CoreModelBase
{
    // Id is coin market Id
    public string? Name { get; set; }
    public string? Slug { get; set; }
    public string? Symbol { get; set; }

    public float Price { get; set; }

    public DateTime CoinMarketLastUpdated { get; set; }
    public DateTime LastUpdated { get; set; }
    public DateTime NextUpdated { get; set; }
}
