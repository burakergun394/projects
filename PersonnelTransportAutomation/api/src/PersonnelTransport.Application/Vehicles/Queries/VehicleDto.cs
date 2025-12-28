using PersonnelTransport.Domain.Common;
using PersonnelTransport.Domain.Vehicles;

namespace PersonnelTransport.Application.Vehicles.Queries;

public record VehicleDto(Guid Id, string PlateNumber, int Capacity, Location DepotLocation, VehicleType Type, double CostPerKm);
