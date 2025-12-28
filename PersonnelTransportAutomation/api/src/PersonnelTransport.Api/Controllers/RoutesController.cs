using Microsoft.AspNetCore.Mvc;
using PersonnelTransport.Application.Routes.Commands.CreateRoute;
using PersonnelTransport.Application.Routes.Queries;
using PersonnelTransport.Application.Routes.Queries.GetActiveRoutes;
using Space.Abstraction;

namespace PersonnelTransport.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RoutesController(ISpace space) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create(CreateRouteCommand command, CancellationToken cancellationToken)
    {
        var id = await space.Send<Guid>(command, ct: cancellationToken);
        // Route doesn't have GetById yet implemented effectively (GetActiveRoutes is list), 
        // but let's assume getting by id is useful. I'll stick to Created (201) with just ID for now or CreatedAtAction if I add GetById/
        // Actually I removed GetById from interface? No, IRouteRepository has GetByIdAsync but I didn't make a Handler for it yet.
        // Return 200/201 without location for now or 201 with id.
        return Ok(id);
    }

    [HttpGet("active")]
    public async Task<IActionResult> GetActiveRoutes(CancellationToken cancellationToken)
    {
        var query = new GetActiveRoutesQuery();
        var routes = await space.Send<List<RouteDto>>(query, ct: cancellationToken);
        return Ok(routes);
    }
}
