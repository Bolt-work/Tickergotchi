using System.ComponentModel.DataAnnotations;

namespace BlazorUI.Models;

public class FeedGotchiUiModel
{
    [Required]
    public string? GotchiId { get; set; }

    [Required]
    public string? PortfolioId { get; set; }
}
