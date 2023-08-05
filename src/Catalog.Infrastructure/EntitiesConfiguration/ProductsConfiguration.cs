using Catalog.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.Infrastructure.EntitiesConfiguration
{
    public class ProductsConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Name).HasMaxLength(Product.NameMaxLength).IsRequired();
            builder.Property(p => p.Description).HasMaxLength(Product.DescriptionMaxLength).IsRequired();
            builder.Property(p => p.Price).HasPrecision(10, 2).IsRequired();
            builder.Property(p => p.ImageUrl).HasMaxLength(Product.ImageUrlMaxLength).IsRequired();
            builder.Property(p => p.Stock).IsRequired();
            builder.Property(p => p.RegistrationDate).IsRequired();

            builder.HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId);

            builder.HasData(
                new Product(1, "Caneta", "Caneta esferográfica", 2.00m, "caneta.jpg", 50, DateTime.Now, 1),
                new Product(2, "Lápis", "Lápis HB", 1.00m, "lapis.jpg", 70, DateTime.Now, 1),
                new Product(3, "Borracha", "Borracha branca", 1.50m, "borracha.jpg", 30, DateTime.Now, 1),
                new Product(4, "Notebook", "Notebook 15 polegadas", 3000.00m, "notebook.jpg", 5, DateTime.Now, 2),
                new Product(5, "Tablet", "Tablet 10 polegadas", 2500.00m, "tablet.jpg", 10, DateTime.Now, 2),
                new Product(6, "Celular", "Celular 5 polegadas", 1500.00m, "celular.jpg", 15, DateTime.Now, 2),
                new Product(7, "Bolsa", "Bolsa de couro", 500.00m, "bolsa.jpg", 20, DateTime.Now, 3),
                new Product(8, "Carteira", "Carteira de couro", 700.00m, "carteira.jpg", 40, DateTime.Now, 3),
                new Product(9, "Cinto", "Cinto de couro", 400.00m, "cinto.jpg", 60, DateTime.Now, 3)
            );
        }
    }
}
