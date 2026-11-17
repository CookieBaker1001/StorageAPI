using System.ComponentModel.DataAnnotations;

namespace StorageAPI.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Price { get; set; }
        public string Category { get; set; } = string.Empty;
        public string Shelf { get; set; } = string.Empty;
        public int Count { get; set; }
        [MaxLength(500)]
        public string? Description { get; set; }
    }
}
