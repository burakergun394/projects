using Application.Features.Products.Commands.Create;
using Application.Features.Products.Commands.Update;
using Application.Features.Products.Queries.GetById;
using Application.Features.Products.Queries.Search;
using Domain.Paginations;
using Domain.Products;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Presentation.Web.API.Controller.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class ProductsController : ApiController
{
    private readonly ISender _sender;

    public ProductsController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet]
    public async Task<ActionResult<PagedResponse<Product>>> Search([FromQuery] ProductSearchQuery request)
    {
        var response = await _sender.Send(request);
        return response;
    }


    [HttpGet]
    [Route("{id}")]
    public async Task<ActionResult<Product>> CreateAsync(Guid id)
    {
        var response = await _sender.Send(new ProductGetByIdQuery(id));
        return response;
    }

    [HttpPost]
    [Route("create")]
    public async Task<ActionResult<Product>> CreateAsync([FromBody] ProductCreateCommand request)
    {
        var response = await _sender.Send(request);
        return response;
    }

    [HttpPost]
    [Route("update")]
    public async Task<ActionResult<Product>> UpdateAsync([FromBody] ProductUpdateCommand request)
    {
        var response = await _sender.Send(request);
        return response;
    }
}