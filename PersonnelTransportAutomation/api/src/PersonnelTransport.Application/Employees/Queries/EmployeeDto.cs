using PersonnelTransport.Domain.Common;

namespace PersonnelTransport.Application.Employees.Queries;

public record EmployeeDto(Guid Id,
                          string FirstName,
                          string LastName,
                          Location HomeLocation,
                          Shift Shift,
                          Guid? AssignedRouteId,
                          List<string> SpecialNeeds);
