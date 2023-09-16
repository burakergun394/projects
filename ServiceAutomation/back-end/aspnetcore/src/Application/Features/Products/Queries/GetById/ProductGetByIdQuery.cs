using Application.Behaviors.Authorization;
using Domain.Products;
using MediatR;

namespace Application.Features.Products.Queries.GetById;

public record ProductGetByIdQuery(Guid Id) : IRequest<Product>, IAuthorizationClaimRequest
{
    public string Claim { get; } = "product.get";
};

public class ProductGetByIdQueryHandler : IRequestHandler<ProductGetByIdQuery, Product>
{
    private readonly IProductRepository _productRepository;
    public ProductGetByIdQueryHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<Product> Handle(ProductGetByIdQuery request, CancellationToken cancellationToken)
    {
        return await _productRepository.GetByIdAsync(request.Id, cancellationToken);
    }
}