using Microsoft.AspNetCore.Mvc;
using Space.Abstraction;

namespace FollowCatcher.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TwitterController(ISpace space) : ControllerBase
{
}
