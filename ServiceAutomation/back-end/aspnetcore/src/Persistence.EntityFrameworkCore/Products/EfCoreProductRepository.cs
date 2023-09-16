using Ardalis.GuardClauses;
using Domain.Products;
using Domain.Shared.Paginations;
using Persistence.EntityFrameworkCore.Shared;
using Persistence.EntityFrameworkCore.Shared.Helpers;

namespace Persistence.EntityFrameworkCore.Products;

internal class EfCoreProductRepository : EfCoreRepository<Product, Guid, ApplicationDbContext>, IProductRepository
{
    public EfCoreProductRepository(ApplicationDbContext context) : base(context)
    {

    }

    public Task<PagedResponse<Product>> SearchAsync(ProductSearch search)
    {
        Guard.Against.Null(search);
        
        IQueryable<Product> query = DbSet;

        if (!string.IsNullOrWhiteSpace(search.Code))
            query = query.Where(x => x.Code.Contains(search.Code));

        if (!string.IsNullOrWhiteSpace(search.Name))
            query = query.Where(x => x.Name.Contains(search.Name));

        if (!string.IsNullOrWhiteSpace(search.Category))
            query = query.Where(x => x.Category.Contains(search.Category));

        if (!string.IsNullOrWhiteSpace(search.Explanation))
            query = query.Where(x => x.Explanation.Contains(search.Explanation));

        if (search.Price.HasValue)
            query = query.Where(x => x.Price == search.Price.Value);

        if (search.Stock.HasValue)
            query = query.Where(x => x.Stock == search.Stock.Value);

        return PaginationHelper.CreatePagedResponseFromQueryableAsync(query, search.Page, search.PageSize);
    }
}
