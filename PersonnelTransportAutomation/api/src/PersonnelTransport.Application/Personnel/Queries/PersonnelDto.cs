namespace PersonnelTransport.Application.Personnel.Queries;

using PersonnelTransport.Domain.Common;

public record PersonnelDto(Guid Id, string FirstName, string LastName, Location HomeLocation, Shift Shift, Guid? AssignedRouteId, List<string> SpecialNeeds);
