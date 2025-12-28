using PersonnelTransport.Domain.Vehicles;
using Space.Abstraction.Attributes;
using Space.Abstraction.Context;

namespace PersonnelTransport.Application.Vehicles.Commands.CreateVehicle;

public class CreateVehicleHandler(IVehicleRepository repository)
{
    [Handle]
    public async ValueTask<Guid> HandleAsync(HandlerContext<CreateVehicleCommand> ctx)
    {
        var request = ctx.Request;
        var vehicle = new Vehicle(
            Guid.NewGuid(),
            request.PlateNumber,
            request.Capacity,
            request.DepotLocation,
            request.Type,
            request.CostPerKm
        );

        await repository.AddAsync(vehicle, ctx.CancellationToken);

        return vehicle.Id;
    }
}
