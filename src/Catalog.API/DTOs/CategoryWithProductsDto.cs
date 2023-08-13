namespace Catalog.API.DTOs
{
    public class CategoryWithProductsDto : ICategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public IEnumerable<ProductDto> Products { get; set; } = new List<ProductDto>();
    }
}
