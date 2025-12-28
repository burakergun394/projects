using PersonnelTransport.Domain.Common;

namespace PersonnelTransport.Domain.Routes;

public class RouteStop : Entity
{
    public int Order { get; private set; }
    public Location Location { get; private set; } = default!;
    public DateTime EstimatedArrival { get; private set; }
    public List<Guid> PickupEmployeeIds { get; private set; } = [];

    public RouteStop(Guid id, int order, Location location, DateTime estimatedArrival, List<Guid> pickupEmployeeIds) : base(id)
    {
        Order = order;
        Location = location;
        EstimatedArrival = estimatedArrival;
        PickupEmployeeIds = pickupEmployeeIds;
    }

    private RouteStop() { }
}
