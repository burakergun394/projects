using Domain.Paginations;

namespace Domain.Products;

public sealed class ProductSearch : PagedRequest
{
    public string? Code { get; private set; }
    public string? Name { get; private set; }
    public string? Category { get; private set; }
    public string? Explanation { get; private set; }
    public decimal? Price { get; private set; }
    public int? Stock { get; private set; }

    public ProductSearch(int page, int pageSize, string? code, string? name, string? category, string? explanation, decimal? price, int? stock) : base(page, pageSize)
    {
        Code = code;
        Name = name;
        Category = category;
        Explanation = explanation;
        Price = price;
        Stock = stock;
    }
}