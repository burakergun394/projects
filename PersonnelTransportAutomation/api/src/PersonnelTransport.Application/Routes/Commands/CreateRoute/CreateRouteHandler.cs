using Space.Abstraction.Attributes;
using Space.Abstraction.Context;
using PersonnelTransport.Domain.Routes;

namespace PersonnelTransport.Application.Routes.Commands.CreateRoute;

public class CreateRouteHandler(IRouteRepository repository)
{
    [Handle]
    public async ValueTask<Guid> HandleAsync(HandlerContext<CreateRouteCommand> ctx)
    {
        var request = ctx.Request;
        var route = new Route(
            Guid.NewGuid(),
            request.VehicleId,
            request.StartTime,
            request.EndTime
        );

        await repository.AddAsync(route, ctx.CancellationToken);

        return route.Id;
    }
}
