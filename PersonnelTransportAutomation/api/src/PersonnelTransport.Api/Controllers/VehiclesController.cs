using Microsoft.AspNetCore.Mvc;
using PersonnelTransport.Application.Vehicles.Commands.CreateVehicle;
using PersonnelTransport.Application.Vehicles.Queries;
using PersonnelTransport.Application.Vehicles.Queries.GetVehicleById;
using Space.Abstraction;

namespace PersonnelTransport.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VehiclesController(ISpace space) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create(CreateVehicleCommand command, CancellationToken cancellationToken)
    {
        var id = await space.Send<Guid>(command, ct: cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id }, id);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var query = new GetVehicleByIdQuery(id);
        var vehicle = await space.Send<VehicleDto>(query, ct: cancellationToken);

        if (vehicle == null) return NotFound();

        return Ok(vehicle);
    }
}
