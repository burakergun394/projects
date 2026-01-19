using FollowCatcher.Application.Instagram.Dtos;
using FollowCatcher.Application.Instagram.Queries.GetInstagramProfile;
using Microsoft.AspNetCore.Mvc;
using Space.Abstraction;

namespace FollowCatcher.Api.Controllers;


[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class InstagramController(ISpace space) : ControllerBase
{

    [HttpGet("{username}")]
    [ProducesResponseType(typeof(InstagramProfileDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<InstagramProfileDto>> GetProfile(
        string username,
        [FromQuery] bool includeProfileCard = false,
        CancellationToken cancellationToken = default)
    {
        var result = await space.Send(new GetInstagramProfileQuery(username, includeProfileCard), ct: cancellationToken);
        return Ok(result);
    }
}
