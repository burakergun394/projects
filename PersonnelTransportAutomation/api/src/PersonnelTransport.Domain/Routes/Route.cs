namespace PersonnelTransport.Domain.Routes;

using PersonnelTransport.Domain.Common;

public class Route : Entity
{
    public Guid VehicleId { get; private set; }
    public List<RouteStop> Stops { get; private set; } = [];
    public DateTime StartTime { get; private set; }
    public DateTime EndTime { get; private set; }
    public double TotalDistanceMeters { get; private set; }
    public double ExpectedDurationSeconds { get; private set; }
    public RouteStatus Status { get; private set; }

    public Route(Guid id, Guid vehicleId, DateTime startTime, DateTime endTime) : base(id)
    {
        VehicleId = vehicleId;
        StartTime = startTime;
        EndTime = endTime;
        Status = RouteStatus.Planning;
        Stops = [];
    }

    private Route() { } // EF Core

    public void AddStop(RouteStop stop)
    {
        Stops.Add(stop);
        // Logic to re-order could go here
    }

    public void UpdateMetrics(double totalDistance, double totalDuration)
    {
        TotalDistanceMeters = totalDistance;
        ExpectedDurationSeconds = totalDuration;
    }

    public void StartRoute()
    {
        Status = RouteStatus.Active;
    }

    public void CompleteRoute()
    {
        Status = RouteStatus.Completed;
    }
}
