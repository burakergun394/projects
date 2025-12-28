namespace PersonnelTransport.Application.Employees.Commands.CreateEmployee;

using PersonnelTransport.Domain.Common;

public record CreateEmployeeCommand(string FirstName,
                                    string LastName,
                                    Location HomeLocation,
                                    Shift Shift,
                                    List<string> SpecialNeeds);
