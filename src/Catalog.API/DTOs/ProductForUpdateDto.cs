using Catalog.Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace Catalog.API.DTOs
{
    public class ProductForUpdateDto
    {
        public int Id { get; set; }

        [MaxLength(Product.NameMaxLength)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(Product.DescriptionMaxLength)]
        public string Description { get; set; } = string.Empty;

        [Range((double)Product.MinPrice, (double)Product.MaxPrice)]
        public decimal Price { get; set; }

        [MaxLength(Product.ImageUrlMaxLength)]
        public string ImageUrl { get; set; } = string.Empty;

        [Range(Product.MinStock, Product.MaxStock)]
        public int Stock { get; set; }
        public DateTime RegistrationDate { get; set; }
        public int CategoryId { get; set; }
    }
}
