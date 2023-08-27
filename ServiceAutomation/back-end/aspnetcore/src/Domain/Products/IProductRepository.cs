using Domain.Paginations;
using Domain.Shared;

namespace Domain.Products;

public interface IProductRepository : IRepository<Product, Guid>
{
    Task<PagedResponse<Product>> SearchAsync(ProductSearch search);
}