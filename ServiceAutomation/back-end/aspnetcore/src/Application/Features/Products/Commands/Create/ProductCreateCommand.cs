﻿using Application.Behaviors.Authorization;
using Domain.Products;
using MediatR;

namespace Application.Features.Products.Commands.Create;

public record ProductCreateCommand(string Code, string Name, string Category, string Explanation, decimal Price, int Stock) : IRequest<Product>, IAuthorizationClaimRequest
{
    public string Claim { get; } = "product.create";
};

public class ProductCreateCommandHandler : IRequestHandler<ProductCreateCommand, Product>
{
    private readonly IProductRepository _productRepository;


    public ProductCreateCommandHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<Product> Handle(ProductCreateCommand request, CancellationToken cancellationToken)
    {
        var addingProduct = Product.Create(request.Code, request.Name, request.Category, request.Explanation, request.Price, request.Stock);
        var addedProduct = await _productRepository.CreateAsync(addingProduct, cancellationToken);
        await _productRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        return addedProduct;
    }
}

