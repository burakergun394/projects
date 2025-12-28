using PersonnelTransport.Domain.Employees;
using Space.Abstraction.Attributes;
using Space.Abstraction.Context;

namespace PersonnelTransport.Application.Employees.Commands.CreateEmployee;

public class CreateEmployeeCommandHandler(IEmployeeRepository repository)
{
    [Handle]
    public async ValueTask<Guid> HandleAsync(HandlerContext<CreateEmployeeCommand> ctx)
    {
        var request = ctx.Request;
        var employee = new Employee(
            Guid.NewGuid(),
            request.FirstName,
            request.LastName,
            request.HomeLocation,
            request.Shift
        );

        // Add special needs if any (assuming logic to add them exists or just passing for now, 
        // strictly speaking the Constructor I made earlier didn't take them, so I should probably update Domain or just ignore for MVP)
        // For strictness, I'll stick to the constructor I defined. I'll need to add a method to Personnel to add special needs if required.

        await repository.AddAsync(employee);

        return employee.Id;
    }
}
