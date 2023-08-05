using Catalog.Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace Catalog.API.DTOs
{
    public class CategoryForUpdateDto
    {
        public int Id { get; set; }

        [MaxLength(Category.NameMaxLength)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(Category.ImageUrlMaxLength)]
        public string ImageUrl { get; set; } = string.Empty;
    }
}
