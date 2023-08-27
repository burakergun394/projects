using Domain.Products;
using MediatR;

namespace Application.Products.Queries.GetById;

public record ProductGetByIdQuery(Guid Id) : IRequest<Product>;

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