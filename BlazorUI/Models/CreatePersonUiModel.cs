using System.ComponentModel.DataAnnotations;

namespace BlazorUI.Models;

public class CreatePersonUiModel
{
    [Required]
    public string? PersonId;

    [Required]
    public string? FirstName { get; set; } = null!;

    [Required]
    public string? LastName { get; set; } = null!;
}
