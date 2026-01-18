using FollowCatcher.Application.Instagram.Queries;
using FollowCatcher.Application.Instagram.Queries.GetInstagramProfile;
using FollowCatcher.Application.Instagram.Queries.GetProfileCard;
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

    [HttpGet("{username}/card")]
    [Produces("image/png")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetProfileCard(
        string username,
        CancellationToken cancellationToken = default)
    {
        var imageBytes = await mediator.Send(new GetProfileCardQuery(username), cancellationToken);

        if (imageBytes is null)
        {
            return NotFound();
        }

        return File(imageBytes, "image/png");
    }
}
