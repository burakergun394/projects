using PersonnelTransport.Domain.Employees;
using Space.Abstraction.Attributes;
using Space.Abstraction.Context;

namespace PersonnelTransport.Application.Employees.Queries.GetPersonnelById;

public class GetEmployeeByIdHandler(IEmployeeRepository repository)
{
    [Handle]
    public async ValueTask<EmployeeDto?> HandleAsync(HandlerContext<GetEmployeeByIdQuery> ctx)
    {
        var request = ctx.Request;
        var entity = await repository.GetByIdAsync(request.Id);

        if (entity == null) 
            return null;

        return new EmployeeDto(
            entity.Id,
            entity.FirstName,
            entity.LastName,
            entity.HomeLocation,
            entity.Shift,
            entity.AssignedRouteId,
            entity.SpecialNeeds
        );
    }
}
