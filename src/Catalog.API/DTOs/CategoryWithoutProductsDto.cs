namespace Catalog.API.DTOs
{
    public class CategoryWithoutProductsDto : ICategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
    }
}
