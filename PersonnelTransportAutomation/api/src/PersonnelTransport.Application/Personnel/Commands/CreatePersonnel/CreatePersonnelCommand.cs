namespace PersonnelTransport.Application.Personnel.Commands.CreatePersonnel;

using Space.Abstraction;
using PersonnelTransport.Domain.Common;

[Command]
public record CreatePersonnelCommand(string FirstName, string LastName, Location HomeLocation, Shift Shift, List<string> SpecialNeeds) : ICommand<Guid>;
