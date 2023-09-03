using Application.Roles.Commands.Create;
using Application.Roles.Queries.GetById;
using Domain.Claims;
using Domain.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Presentation.Web.API.Controller.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class ClaimsController : ApiController
{
    private readonly ISender _sender;

    public ClaimsController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<ActionResult<Response<Claim>>> GetAsync(Guid id)
    {
        var response = await _sender.Send(new ClaimGetByIdQuery(id));
        return response;
    }

    [HttpPost]
    [Route("create")]
    public async Task<ActionResult<Response<Claim>>> CreateAsync([FromBody] ClaimCreateCommand request)
    {
        var response = await _sender.Send(request);
        return response;
    }
}
