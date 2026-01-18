using FollowCatcher.Application.Instagram.Queries;
using FollowCatcher.Application.Instagram.Queries.GetInstagramProfile;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FollowCatcher.Api.Controllers;

/// <summary>
/// Controller for Instagram-related operations.
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class InstagramController(IMediator mediator) : ControllerBase
{
    /// <summary>
    /// Gets Instagram profile information for a given username.
    /// </summary>
    /// <param name="username">The Instagram username to fetch profile information for.</param>
    /// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>Instagram profile information including follower count, following count, profile picture URL, and post count.</returns>
    /// <response code="200">Returns the Instagram profile information</response>
    /// <response code="404">If the profile is not found</response>
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
