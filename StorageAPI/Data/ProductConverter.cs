using StorageAPI.DTO;
using StorageAPI.Models;

namespace StorageAPI.Data
{
    public static class ProductConverter
    {
        public static CreateProductDto ToProductDto(Product product)
        {
            return new CreateProductDto
            {
                Name = product.Name,
                Price = product.Price,
                Category = product.Category,
                Shelf = product.Shelf,
                Count = product.Count,
                Description = product.Description
            };
        }

        public static Product ToProduct(CreateProductDto dto)
        {
            return new Product
            {
                Name = dto.Name,
                Price = dto.Price,
                Category = dto.Category,
                Shelf = dto.Shelf,
                Count = dto.Count,
                Description = dto.Description
            };
        }
    }
}
