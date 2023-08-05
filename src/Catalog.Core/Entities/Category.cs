using Catalog.Core.Exceptions;

namespace Catalog.Core.Entities
{
    public sealed class Category : Entity
    {
        public const int NameMaxLength = 100;
        public const int ImageUrlMaxLength = 250;

        public string Name { get; private set; } = string.Empty;
        public string ImageUrl { get; private set; } = string.Empty;
        public ICollection<Product> Products { get; private set; } = new List<Product>();

        public Category(string name, string imageUrl)
        {
            ValidateDomain(name, imageUrl);
        }

        public Category(int id, string name, string imageUrl)
        {
            ValidationException.When(id < 0,
                "Invalid Id value.");

            Id = id;

            ValidateDomain(name, imageUrl);
        }

        private void ValidateDomain(string name, string imageUrl)
        {
            ValidationException.When(string.IsNullOrWhiteSpace(name),
                "Name is required.");

            ValidationException.When(name.Length > NameMaxLength,
                $"Name must not exceed {NameMaxLength} characters.");

            ValidationException.When(string.IsNullOrWhiteSpace(imageUrl),
                "Image URL is required.");

            ValidationException.When(imageUrl.Length > ImageUrlMaxLength,
                $"Image URL must not exceed {ImageUrlMaxLength} characters.");

            Name = name;
            ImageUrl = imageUrl;
        }

        public void Update(string name, string imageUrl)
        {
            ValidateDomain(name, imageUrl);
        }
    }
}
