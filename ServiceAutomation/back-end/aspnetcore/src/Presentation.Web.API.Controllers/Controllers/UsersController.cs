using Application.Features.Users.Commands.Create;
using Application.Features.Users.Commands.Login;
using Application.Features.Users.Commands.UpdatePassword;
using Application.Features.Users.Queries.GetById;
using Domain.Responses;
using Domain.Users;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Presentation.Web.API.Controller.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class UsersController : ApiController
{
    private readonly ISender _sender;

    public UsersController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<ActionResult<Response<User>>> GetAsync(Guid id)
    {
        var response = await _sender.Send(new UserGetByIdQuery(id));
        return response;
    }

    [HttpPost]
    [Route("create")]
    public async Task<ActionResult<Response<User>>> CreateAsync([FromBody] UserCreateCommand request)
    {
        var response = await _sender.Send(request);
        return response;
    }

    [HttpPost]
    [Route("updatepassword")]
    public async Task<ActionResult<Response<NoContentResponse>>> UpdatePasswordAsync([FromBody] UpdateUserPasswordCommand request)
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
}