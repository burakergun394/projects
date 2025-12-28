namespace PersonnelTransport.Domain.Routes;

using PersonnelTransport.Domain.Common;

public class RouteStop : Entity
{
    public int Order { get; private set; }
    public Location Location { get; private set; } = default!;
    public DateTime EstimatedArrival { get; private set; }
    public List<Guid> PickupPersonnelIds { get; private set; } = [];

    public RouteStop(Guid id, int order, Location location, DateTime estimatedArrival, List<Guid> pickupPersonnelIds) : base(id)
    {
        Order = order;
        Location = location;
        EstimatedArrival = estimatedArrival;
        PickupPersonnelIds = pickupPersonnelIds;
    }

    private RouteStop() { }
}
