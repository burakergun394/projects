using PersonnelTransport.Domain.Common;
using PersonnelTransport.Domain.Vehicles;

namespace PersonnelTransport.Application.Vehicles.Commands.CreateVehicle;

public record CreateVehicleCommand(string PlateNumber, int Capacity, Location DepotLocation, VehicleType Type, double CostPerKm);
