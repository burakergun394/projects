using Domain.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.EntityFrameworkCore.Shared.EntityTypeConfiguratinos;

namespace Persistence.EntityFrameworkCore.Products;

internal class ProductEntityTypeConfiguration : TenantAuditableEntityTypeConfiguration<Product, Guid>
{
    public override void TenantAuditableEntityBuilder(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("PRODUCT", EntityFrameworkCoreConstants.DefaultSchemaName);

        builder.Property(x => x.Code)
            .HasColumnName("Code")
            .HasColumnType("varchar(50)")
            .IsRequired();

        builder.Property(x => x.Name)
            .HasColumnName("Name")
            .HasColumnType("varchar(500)")
            .IsRequired();

        builder.Property(x => x.Category)
            .HasColumnName("Category")
            .HasColumnType("varchar(50)")
            .IsRequired();

        builder.Property(x => x.Explanation)
            .HasColumnName("Explanation")
            .HasColumnType("varchar(1000)")
            .IsRequired();

        builder.Property(x => x.Price)
            .HasColumnName("Price")
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(x => x.Stock)
            .HasColumnName("Stock")
            .HasColumnType("int")
            .IsRequired();

        var data = new List<Product>();
        foreach (var i in Enumerable.Range(1, 10))
        {
            var product = Product.Create($"Code{i}", $"Name{i}", $"Category{i}", $"Explanation{i}", i, i);
            product.TenantCode = "DEFAULT";
            product.CreatedBy = "DEFAULT";
            product.CreatedAt = DateTime.UtcNow;
            product.LastUpdatedBy = "";
            product.LastUpdatedAt = default;
            data.Add(product);
        }

        builder.HasData(data);
    }
}
