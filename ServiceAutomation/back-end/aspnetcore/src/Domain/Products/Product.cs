using Domain.Shared.Entities;
using Domain.Shared.Enums;

namespace Domain.Products;

public sealed class Product : TenantAuditableEntity<Guid>
{
    public string Code { get; private set; }
    public string Name { get; private set; }
    public string Category { get; private set; }
    public string Explanation { get; private set; }
    public decimal Price { get; private set; }
    public int Stock { get; private set; }

    public Product() { }

    public Product(Guid guid,
                   Status status,
                   string code,
                   string name,
                   string category,
                   string explanation,
                   decimal price,
                   int stock) : base(guid, status)
    {
        Code = code;
        Name = name;
        Category = category;
        Explanation = explanation;
        Price = price;
        Stock = stock;
    }

    public static Product Create(string code,
                                 string name,
                                 string category,
                                 string explanation,
                                 decimal price,
                                 int stock)
    {
        var product = new Product(Guid.NewGuid(), Status.Active, code, name, category, explanation, price, stock)
        {
            TenantCode = "",
            CreatedBy = "",
            CreatedAt = default,
            LastUpdatedBy = "",
            LastUpdatedAt = default
        };

        return product;
    }

    public Product Update(Status status,
                          string code,
                          string name,
                          string category,
                          string explanation,
                          decimal price,
                          int stock)
    {

        var product = new Product(Id, status, code, name, category, explanation, price, stock)
        {
            TenantCode = TenantCode,
            CreatedBy = CreatedBy,
            CreatedAt = CreatedAt,
            LastUpdatedBy = LastUpdatedBy,
            LastUpdatedAt = LastUpdatedAt
        };

        return product;
    }
}