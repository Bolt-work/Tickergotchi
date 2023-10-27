using System.ComponentModel.DataAnnotations;

namespace BlazorUI.Models;

public class CreateNewPortfolioUiModel
{
    [Required]
    public string? PersonId;
}
