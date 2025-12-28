using PersonnelTransport.Domain.Common;

namespace PersonnelTransport.Application.Employees.Commands.CreateEmployee;

public record CreateEmployeeCommand(string FirstName, string LastName, Location HomeLocation, Shift Shift, List<string> SpecialNeeds);
