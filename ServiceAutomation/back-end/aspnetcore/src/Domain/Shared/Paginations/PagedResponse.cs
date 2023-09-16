namespace Domain.Shared.Paginations;

public class PagedResponse<T>
{
    public List<T> Items { get; }
    public int TotalCount { get; }
    public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);
    public int CurrentPage { get; }
    public int PageSize { get; }
    public bool HasNextPage => CurrentPage * PageSize < TotalCount;
    public bool HasPreviousPage => CurrentPage > 1;

    private PagedResponse(List<T> ıtems, int totalCount, int currentPage, int pageSize)
    {
        Items = ıtems;
        TotalCount = totalCount;
        CurrentPage = currentPage;
        PageSize = pageSize;
    }

    public static PagedResponse<T> CreatePagedResponse(List<T> items, int totalCount, int currentPage, int pageSize)
    {
        return new PagedResponse<T>(items, totalCount, currentPage, pageSize);
    }
}

