namespace Gotchi.Portfolios.Models;

public class Asset
{
    public string? Id { get; set; }
    public string? CoinMarketId { get; set; }
    public string? Name { get; set; }
    public string? Slug { get; set; }
    public string? Symbol { get; set; }
    public float PriceWhenLastBought { get; set; }
    public float Profit { get; set; }
    public float Units { get; set; }
}
