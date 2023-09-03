using Application.Behaviors.Authorization;
using Domain.Paginations;
using Domain.Products;
using MediatR;

namespace Application.Features.Products.Queries.Search;

public class ProductSearchQuery : PagedRequest, IRequest<PagedResponse<Product>>, IAuthorizationClaimRequest
{
    public string? Code { get; set; }
    public string? Name { get; set; }
    public string? Category { get; set; }
    public string? Explanation { get; set; }
    public decimal? Price { get; set; }
    public int? Stock { get; set; }
    public string Claim { get; } = "product.search";
}

public class ProductSearchQueryHandler : IRequestHandler<ProductSearchQuery, PagedResponse<Product>>
{
    private readonly IProductRepository _productRepository;

    public ProductSearchQueryHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<PagedResponse<Product>> Handle(ProductSearchQuery request, CancellationToken cancellationToken)
    {
        var search = new ProductSearch(request.Page, request.PageSize, request.Code, request.Name, request.Category, request.Explanation, request.Price, request.Stock);
        return await _productRepository.SearchAsync(search);
    }
}