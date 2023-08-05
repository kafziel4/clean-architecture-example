using Catalog.Core.Exceptions;

namespace Catalog.Core.Entities
{
    public sealed class Product : Entity
    {
        public const int NameMaxLength = 100;
        public const int DescriptionMaxLength = 200;
        public const decimal MinPrice = 0.00m;
        public const decimal MaxPrice = 1_000_000_000.00m;
        public const int ImageUrlMaxLength = 250;
        public const int MinStock = 0;
        public const int MaxStock = 10_000;

        public string Name { get; private set; } = string.Empty;
        public string Description { get; private set; } = string.Empty;
        public decimal Price { get; private set; }
        public string ImageUrl { get; private set; } = string.Empty;
        public int Stock { get; private set; }
        public DateTime RegistrationDate { get; private set; }
        public int CategoryId { get; private set; }
        public Category? Category { get; private set; }

        public Product(string name,
            string description,
            decimal price,
            string imageUrl,
            int stock,
            DateTime registrationDate)
        {
            ValidateDomain(
                name, description, price, imageUrl, stock, registrationDate);
        }

        public Product(int id,
            string name,
            string description,
            decimal price,
            string imageUrl,
            int stock,
            DateTime registrationDate,
            int categoryId)
        {
            ValidationException.When(id < 0,
                "Invalid Id value.");

            ValidationException.When(categoryId < 0,
                "Invalid Category Id value.");

            Id = id;
            CategoryId = categoryId;

            ValidateDomain(
                name, description, price, imageUrl, stock, registrationDate);
        }

        private void ValidateDomain(string name,
            string description,
            decimal price,
            string imageUrl,
            int stock,
            DateTime registrationDate)
        {
            ValidationException.When(string.IsNullOrWhiteSpace(name),
                "Name is required.");

            ValidationException.When(name.Length > NameMaxLength,
                $"Name must not exceed {NameMaxLength} characters.");

            ValidationException.When(string.IsNullOrWhiteSpace(name),
                "Description is required.");

            ValidationException.When(description.Length > DescriptionMaxLength,
                $"Description must not exceed {DescriptionMaxLength} characters.");

            ValidationException.When(price < MinPrice,
                "Price must not be negative.");

            ValidationException.When(price > MaxPrice,
                $"Price above maximum {MaxPrice} value.");

            ValidationException.When(string.IsNullOrWhiteSpace(imageUrl),
                "Image URL is required.");

            ValidationException.When(imageUrl.Length > ImageUrlMaxLength,
                $"Image URL must not exceed {ImageUrlMaxLength} characters.");

            ValidationException.When(stock < MinStock,
                "Stock must not be negative.");

            ValidationException.When(stock > MaxStock,
                $"Stock above maximum {MaxStock} value.");

            Name = name;
            Description = description;
            Price = price;
            ImageUrl = imageUrl;
            Stock = stock;
            RegistrationDate = registrationDate;
        }

        public void Update(string name,
            string description,
            decimal price,
            string imageUrl,
            int stock,
            DateTime registrationDate,
            int categoryId)
        {
            ValidateDomain(
                name, description, price, imageUrl, stock, registrationDate);
            CategoryId = categoryId;
        }
    }
}
