using System.ComponentModel.DataAnnotations;

namespace BlazorUI.Models
{
    public class CreateGotchiUiModel
    {

        [Required]
        public string? OwnerId { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "Name is too long.")]
        [MinLength(1, ErrorMessage = "Name is too short.")]
        public string? Name { get; set; }
    }
}
