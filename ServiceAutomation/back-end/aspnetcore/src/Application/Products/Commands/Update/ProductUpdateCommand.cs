using Application.Exceptions;
using Domain.Products;
using Domain.Shared.Enums;
using MediatR;

namespace Application.Products.Commands.Update;

public record ProductUpdateCommand(Guid Id, Status Status, string Code, string Name, string Category, string Explanation, decimal Price, int Stock) : IRequest<Product>;

public class ProductUpdateCommandHandler : IRequestHandler<ProductUpdateCommand, Product>
{
    private readonly IProductRepository _productRepository;


    public ProductUpdateCommandHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<Product> Handle(ProductUpdateCommand request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetAsNoTrackingByIdAsync(request.Id, cancellationToken) 
                            ?? throw new ValueNotFoundException("Product not found");

        var updatingProduct = product.Update(request.Status, request.Code, request.Name, request.Category, request.Explanation, request.Price, request.Stock);
        var updatedProduct = await _productRepository.UpdateAsync(updatingProduct);
        await _productRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        return updatedProduct;
    }
}
