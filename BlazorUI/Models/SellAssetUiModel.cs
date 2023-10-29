using System.ComponentModel.DataAnnotations;

namespace BlazorUI.Models;

public class SellAssetUiModel
{
    [Required(ErrorMessage = "CoinMarket Id is required")]
    public string? CoinMarketId { get; set; }

    [Required(ErrorMessage = "Number of units is required")]
    public float Units { get; set; }
}
