using Application.Roles.Commands.Create;
using Application.Roles.Queries.GetById;
using Application.RolesClaims.Commands.Create;
using Application.RolesClaims.Queries.GetById;
using Domain.Responses;
using Domain.Roles;
using Domain.RolesClaims;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Presentation.Web.API.Controller.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class RolesController : ApiController
{
    private readonly ISender _sender;

    public RolesController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<ActionResult<Response<Role>>> GetAsync(Guid id)
    {
        var response = await _sender.Send(new RoleGetByIdQuery(id));
        return response;
    }

    [HttpPost]
    [Route("create")]
    public async Task<ActionResult<Response<Role>>> CreateAsync([FromBody] RoleCreateCommand request)
    {
        var response = await _sender.Send(request);
        return response;
    }
}

[ApiController]
[Route("/api/[controller]")]
public class RolesClaimsController : ApiController
{
    private readonly ISender _sender;

    public RolesClaimsController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet]
    [Route("getbyroleid")]
    public async Task<ActionResult<Response<RoleClaim>>> GetByRoleIdAsync(Guid roleId)
    {
        var response = await _sender.Send(new RoleClaimGetByRoleIdQuery(roleId));
        return response;
    }

    [HttpPost]
    [Route("create")]
    public async Task<ActionResult<Response<RoleClaim>>> CreateAsync([FromBody] RoleClaimCreateCommand request)
    {
        var response = await _sender.Send(request);
        return response;
    }
}
