using FollowCatcher.Application.Instagram.Commands.CreateInstagramTrackedAccount;
using FollowCatcher.Application.Instagram.Commands.CreateInstagramTrackedAccountsBatch;
using FollowCatcher.Application.Instagram.Commands.DeleteInstagramTrackedAccount;
using FollowCatcher.Application.Instagram.Commands.UpdateInstagramTrackedAccount;
using FollowCatcher.Application.Instagram.Dtos;
using FollowCatcher.Application.Instagram.Queries.GetAllInstagramTrackedAccounts;
using FollowCatcher.Application.Instagram.Queries.GetInstagramTrackedAccountById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FollowCatcher.Api.Controllers;

[ApiController]
[Route("api/instagram-tracked-accounts")]
[Produces("application/json")]
public class InstagramTrackedAccountsController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<InstagramTrackedAccountDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<InstagramTrackedAccountDto>>> GetAll(
        CancellationToken cancellationToken = default)
    {
        var result = await mediator.Send(new GetAllInstagramTrackedAccountsQuery(), cancellationToken);
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(InstagramTrackedAccountDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<InstagramTrackedAccountDto>> GetById(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        var result = await mediator.Send(new GetInstagramTrackedAccountByIdQuery(id), cancellationToken);
        return result is null ? NotFound() : Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Guid>> Create(
        [FromBody] CreateInstagramTrackedAccountCommand command,
        CancellationToken cancellationToken = default)
    {
        var id = await mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id }, id);
    }

    [HttpPost("batch")]
    [ProducesResponseType(typeof(List<Guid>), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<List<Guid>>> CreateBatch(
        [FromBody] CreateInstagramTrackedAccountsBatchCommand command,
        CancellationToken cancellationToken = default)
    {
        var ids = await mediator.Send(command, cancellationToken);
        return Created("", ids);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(
        Guid id,
        [FromBody] UpdateInstagramTrackedAccountCommand command,
        CancellationToken cancellationToken = default)
    {
        if (id != command.Id)
            return BadRequest("ID in URL does not match ID in request body.");

        await mediator.Send(command, cancellationToken);
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        await mediator.Send(new DeleteInstagramTrackedAccountCommand(id), cancellationToken);
        return NoContent();
    }
}
