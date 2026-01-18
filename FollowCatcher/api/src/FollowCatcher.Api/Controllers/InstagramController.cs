using FollowCatcher.Application.Instagram.Queries;
using FollowCatcher.Application.Instagram.Queries.GetInstagramProfile;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FollowCatcher.Api.Controllers;


[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class InstagramController(IMediator mediator) : ControllerBase
{

    [HttpGet("{username}")]
    [ProducesResponseType(typeof(InstagramProfileDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<InstagramProfileDto>> GetProfile(
        string username,
        CancellationToken cancellationToken = default)
    {
        var result = await mediator.Send(new GetInstagramProfileQuery(username), cancellationToken);

        return result is null ? NotFound() : Ok(result);
    }
}
