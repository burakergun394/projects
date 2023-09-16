using Application.Features.Identity.Claims.Commands.Create;
using Application.Features.Identity.Claims.Queries.GetById;
using Application.Features.Identity.Roles.Commands.Create;
using Application.Features.Identity.Roles.Queries.GetById;
using Application.Features.Identity.RolesClaims.Commands.Create;
using Application.Features.Identity.RolesClaims.Queries.GetById;
using Application.Features.Identity.Tenants.Queries.GetActiveList;
using Application.Features.Identity.Users.Commands.Create;
using Application.Features.Identity.Users.Commands.Login;
using Application.Features.Identity.Users.Queries.GetById;
using Domain.Identity.Claims;
using Domain.Identity.Roles;
using Domain.Identity.RolesClaims;
using Domain.Identity.Users;
using Domain.Shared.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Presentation.Web.API.Controller.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class IdentityController : ApiController
{
    private readonly ISender _sender;

    public IdentityController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet]
    [Route("users/{id}")]
    public async Task<ActionResult<Response<User>>> GetUserAsync(Guid id)
    {
        var response = await _sender.Send(new UserGetByIdQuery(id));
        return response;
    }

    [HttpPost]
    [Route("users/create")]
    public async Task<ActionResult<Response<User>>> CreateUserAsync([FromBody] UserCreateCommand request)
    {
        var response = await _sender.Send(request);
        return response;
    }

    [HttpPost]
    [Route("login")]
    public async Task<ActionResult<Response<LoginUserCommandResponse>>> LoginAsync([FromBody] LoginUserCommand request)
    {
        var response = await _sender.Send(request);
        return response;
    }

    [HttpGet]
    [Route("claims/{id}")]
    public async Task<ActionResult<Response<Claim>>> GetClaimAsync(Guid id)
    {
        var response = await _sender.Send(new ClaimGetByIdQuery(id));
        return response;
    }

    [HttpPost]
    [Route("claims/create")]
    public async Task<ActionResult<Response<Claim>>> CreateClaimAsync([FromBody] ClaimCreateCommand request)
    {
        var response = await _sender.Send(request);
        return response;
    }

    [HttpGet]
    [Route("roles/{id}")]
    public async Task<ActionResult<Response<Role>>> GetRoleAsync(Guid id)
    {
        var response = await _sender.Send(new RoleGetByIdQuery(id));
        return response;
    }

    [HttpPost]
    [Route("roles/create")]
    public async Task<ActionResult<Response<Role>>> CreateRoleAsync([FromBody] RoleCreateCommand request)
    {
        var response = await _sender.Send(request);
        return response;
    }

    [HttpPost]
    [Route("roles/system/create")]
    public async Task<ActionResult<Response<Role>>> CreateSystemRoleAsync([FromBody] SystemRoleCreateCommand request)
    {
        var response = await _sender.Send(request);
        return response;
    }

    [HttpGet]
    [Route("roleclaims/getbyroleid")]
    public async Task<ActionResult<Response<RoleClaim>>> GetByRoleIdAsync(Guid roleId)
    {
        var response = await _sender.Send(new RoleClaimGetByRoleIdQuery(roleId));
        return response;
    }

    [HttpPost]
    [Route("roleclaims/create")]
    public async Task<ActionResult<Response<RoleClaim>>> CreateRoleClaimAsync([FromBody] RoleClaimCreateCommand request)
    {
        var response = await _sender.Send(request);
        return response;
    }

    [HttpGet]
    [Route("tenants/getactivetenants")]
    public async Task<ActionResult<Response<List<string>>>> GetActiveListAsync()
    {
        var response = await _sender.Send(new TenantGetActiveListQuery());
        return response;
    }
}
