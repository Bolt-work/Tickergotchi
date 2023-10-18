using Gotchi.Core.Models;

namespace Gotchi.Portfolios.DTOs;
public class AssetDTO : ModelBase
{
    public int CoinMarketId { get; set; }
    public string? Name { get; set; }
    public string? Slug { get; set; }
    public string? Symbol { get; set; }

    public float PriceWhenLastBought { get; set; }
    public float CurrentPrice { get; set; }
    public float Profit { get; set; }
    public float Units { get; set; }
}

