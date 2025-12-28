namespace PersonnelTransport.Domain.Personnel;

using PersonnelTransport.Domain.Common;

public class Personnel : Entity
{
    public string FirstName { get; private set; } = default!;
    public string LastName { get; private set; } = default!;
    public Location HomeLocation { get; private set; } = default!;
    public Shift Shift { get; private set; } = default!;
    public Guid? AssignedRouteId { get; private set; }
    public List<string> SpecialNeeds { get; private set; } = [];

    public Personnel(Guid id, string firstName, string lastName, Location homeLocation, Shift shift) : base(id)
    {
        FirstName = firstName;
        LastName = lastName;
        HomeLocation = homeLocation;
        Shift = shift;
    }

    private Personnel() { } // EF Core

    public void MoveTo(Location newLocation)
    {
        HomeLocation = newLocation;
    }

    public void AssignToRoute(Guid routeId)
    {
        AssignedRouteId = routeId;
    }

    public void ClearRoute()
    {
        AssignedRouteId = null;
    }
}
