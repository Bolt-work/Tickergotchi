﻿namespace Gotchi.Portfolios.Models;

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

    public float CurrentPrice { get; set; }
    public DateTime CoinMarketLastUpdated { get; set; }
    public DateTime CoinLastUpdated { get; set; }
    public DateTime CoinPriceNextUpdated { get; set; }

    public bool IsValid { get; set; }
}
