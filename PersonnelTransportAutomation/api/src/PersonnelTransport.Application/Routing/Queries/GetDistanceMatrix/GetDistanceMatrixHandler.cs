using PersonnelTransport.Application.Contracts;
using Space.Abstraction.Attributes;
using Space.Abstraction.Context;

namespace PersonnelTransport.Application.Routing.Queries.GetDistanceMatrix;

public class GetDistanceMatrixHandler(IRoutingService routingService)
{
    [Handle]
    public async ValueTask<DistanceMatrixResult> HandleAsync(
        HandlerContext<GetDistanceMatrixQuery> context)
    {
        var query = context.Request;

        var origins = query.Origins.Select(o => o.ToDomain()).ToList();
        var destinations = query.Destinations.Select(d => d.ToDomain()).ToList();

        return await routingService.GetDistanceMatrixAsync(
            origins,
            destinations,
            context.CancellationToken);
    }
}
