namespace PersonnelTransport.Domain.Vehicles;

using PersonnelTransport.Domain.Common;

public class Vehicle : Entity
{
    public string PlateNumber { get; private set; } = default!;
    public int Capacity { get; private set; }
    public Location DepotLocation { get; private set; } = default!;
    public VehicleType Type { get; private set; }
    public double CostPerKm { get; private set; }

    public Vehicle(Guid id, string plateNumber, int capacity, Location depotLocation, VehicleType type, double costPerKm) : base(id)
    {
        PlateNumber = plateNumber;
        Capacity = capacity;
        DepotLocation = depotLocation;
        Type = type;
        CostPerKm = costPerKm;
    }

    private Vehicle() { } // EF Core
}
