using Domain.Shared.Paginations;
using Microsoft.EntityFrameworkCore;

namespace Persistence.EntityFrameworkCore.Shared.Helpers;

internal static class PaginationHelper
{
    internal static async Task<PagedResponse<T>> CreatePagedResponseFromQueryableAsync<T>(IQueryable<T> queryable, int page, int pageSize) where T : class, new()
    {
        var totalCount = await queryable.CountAsync();
        var items = await queryable
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return PagedResponse<T>.CreatePagedResponse(items, totalCount, page, pageSize);
    }
}