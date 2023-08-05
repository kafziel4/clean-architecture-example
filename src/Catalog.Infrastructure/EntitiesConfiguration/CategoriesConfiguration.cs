using Catalog.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.Infrastructure.EntitiesConfiguration
{
    public class CategoriesConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Name).HasMaxLength(Category.NameMaxLength).IsRequired();
            builder.Property(c => c.ImageUrl).HasMaxLength(Category.ImageUrlMaxLength).IsRequired();

            builder.HasData(
                new Category(1, "Material Escolar", "material.jpg"),
                new Category(2, "Eletrônicos", "eletronicos.jpg"),
                new Category(3, "Acessórios", "acessorios.jpg")
            );
        }
    }
}
