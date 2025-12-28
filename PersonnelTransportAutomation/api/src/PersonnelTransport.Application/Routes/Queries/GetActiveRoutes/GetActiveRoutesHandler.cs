using PersonnelTransport.Domain.Routes;
using Space.Abstraction.Attributes;
using Space.Abstraction.Context;

namespace PersonnelTransport.Application.Routes.Queries.GetActiveRoutes;

public class GetActiveRoutesHandler(IRouteRepository repository)
{
    [Handle]
    public async ValueTask<List<RouteDto>> HandleAsync(HandlerContext<GetActiveRoutesQuery> ctx)
    {
        var routes = await repository.GetActiveRoutesAsync(ctx.CancellationToken);

        return routes.Select(r => new RouteDto(
            r.Id,
            r.VehicleId,
            r.Stops.Select(s => new RouteStopDto(s.Order, s.Location, s.EstimatedArrival, s.PickupEmployeeIds)).ToList(),
            r.StartTime,
            r.EndTime,
            r.TotalDistanceMeters,
            r.ExpectedDurationSeconds,
            r.Status
        )).ToList();
    }
}
