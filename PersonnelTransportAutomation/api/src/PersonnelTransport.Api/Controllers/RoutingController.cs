using Microsoft.AspNetCore.Mvc;
using PersonnelTransport.Application.Contracts;
using PersonnelTransport.Application.Routing.Queries.GetDistanceMatrix;
using Space.Abstraction;

namespace PersonnelTransport.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RoutingController(ISpace space) : ControllerBase
{
    /// <summary>
    /// Gets the distance matrix between origins and destinations using Google Maps API.
    /// </summary>
    [HttpPost("distance-matrix")]
    public async Task<ActionResult<DistanceMatrixResult>> GetDistanceMatrix(
        [FromBody] GetDistanceMatrixQuery query,
        CancellationToken ct)
    {
        var result = await space.Send<DistanceMatrixResult>(query, ct: ct);
        return Ok(result);
    }
}
