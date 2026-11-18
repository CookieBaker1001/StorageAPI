using System.ComponentModel.DataAnnotations;

namespace StorageAPI.DTO
{
    public class CreateProductDto
    {
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; } = string.Empty;

        [Range(1, 9999, ErrorMessage = "Price must be between 1 and 9999 dollars.")]
        public int Price { get; set; }

        [Required(ErrorMessage = "Category is required.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Category must be between 2 and 50 characters.")]
        public string Category { get; set; } = string.Empty;

        [Required(ErrorMessage = "Shelf-value is required.")]
        [StringLength(25, MinimumLength = 2, ErrorMessage = "Shelf-value must be between 2 and 25 characters.")]
        public string Shelf { get; set; } = string.Empty;

        [Required]
        [Range(0, 999, ErrorMessage = "Count must be between 0 and 999.")]
        public int Count { get; set; }

        [StringLength(500, MinimumLength = 2, ErrorMessage = "Description must be between 2 and 500 characters.")]
        public string? Description { get; set; }
    }
}
