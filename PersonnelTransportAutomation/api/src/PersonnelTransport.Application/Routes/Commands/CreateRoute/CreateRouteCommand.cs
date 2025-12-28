namespace PersonnelTransport.Application.Routes.Commands.CreateRoute;

public record CreateRouteCommand(Guid VehicleId, DateTime StartTime, DateTime EndTime);
