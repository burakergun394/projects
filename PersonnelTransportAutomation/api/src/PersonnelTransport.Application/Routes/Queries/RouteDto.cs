using PersonnelTransport.Domain.Common;
using PersonnelTransport.Domain.Routes;

namespace PersonnelTransport.Application.Routes.Queries;

public record RouteStopDto(int Order, Location Location, DateTime EstimatedArrival, List<Guid> PickupEmployeeIds);
public record RouteDto(Guid Id, Guid VehicleId, List<RouteStopDto> Stops, DateTime StartTime, DateTime EndTime, double TotalDistanceMeters, double ExpectedDurationSeconds, RouteStatus Status);
