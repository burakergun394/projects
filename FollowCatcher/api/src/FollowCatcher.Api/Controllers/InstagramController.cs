using FollowCatcher.Application.Instagram.Dtos;
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
        [FromQuery] bool includeProfileCard = false,
        CancellationToken cancellationToken = default)
    {
        var result = await mediator.Send(new GetInstagramProfileQuery(username, includeProfileCard), cancellationToken);
        return Ok(result);
    }
}
