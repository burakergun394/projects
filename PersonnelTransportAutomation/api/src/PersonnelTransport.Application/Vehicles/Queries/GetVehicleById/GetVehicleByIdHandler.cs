using PersonnelTransport.Domain.Vehicles;
using Space.Abstraction.Attributes;
using Space.Abstraction.Context;

namespace PersonnelTransport.Application.Vehicles.Queries.GetVehicleById;

public class GetVehicleByIdHandler(IVehicleRepository repository)
{
    [Handle]
    public async ValueTask<VehicleDto?> HandleAsync(HandlerContext<GetVehicleByIdQuery> ctx)
    {
        var entity = await repository.GetByIdAsync(ctx.Request.Id, ctx.CancellationToken);

        if (entity == null) return null;

        return new VehicleDto(
            entity.Id,
            entity.PlateNumber,
            entity.Capacity,
            entity.DepotLocation,
            entity.Type,
            entity.CostPerKm
        );
    }
}
