using System.ComponentModel.DataAnnotations;

namespace BlazorUI.Models;

public class BuyAssetUiModel
{
    [Required (ErrorMessage = "CoinMarket Id required")]
    public string? CoinMarketId { get; set; }

    [Required(ErrorMessage = "CoinMarket Id required")]
    [Range(1, int.MaxValue, ErrorMessage = "Value should be greater than or equal to 1")]
    public float AmountInValue { get; set; }
}
