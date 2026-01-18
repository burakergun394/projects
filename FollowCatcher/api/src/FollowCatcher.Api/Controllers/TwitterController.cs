using FollowCatcher.Application.Twitter.Commands.ShareProfileCard;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FollowCatcher.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TwitterController(ISender sender) : ControllerBase
{
    [HttpPost("share-card")]
    public async Task<IActionResult> ShareProfileCard([FromBody] ShareProfileCardCommand command)
    {
        var tweetId = await sender.Send(command);

        if (tweetId == null)
        {
            return BadRequest("Failed to share profile card on Twitter.");
        }

        return Ok(new { TweetId = tweetId });
    }
}
