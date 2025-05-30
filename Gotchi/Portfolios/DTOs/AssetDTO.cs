﻿using Gotchi.Core.Models;

namespace Gotchi.Portfolios.DTOs;
public class AssetDTO : CoreModelBase
{
    public int CoinMarketId { get; set; }
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

    public float NetChange() 
    {
        return (float) CurrentPrice - PriceWhenLastBought;
    }

    public float ProjectedProfit()
    {
        return (float) Units * CurrentPrice;
    }
}

