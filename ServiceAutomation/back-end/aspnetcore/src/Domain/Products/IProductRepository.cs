using Domain.Shared;
using Domain.Shared.Paginations;

namespace Domain.Products;

public interface IProductRepository : IRepository<Product, Guid>
{
    Task<PagedResponse<Product>> SearchAsync(ProductSearch search);
}