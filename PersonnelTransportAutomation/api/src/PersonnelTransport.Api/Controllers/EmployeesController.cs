using Microsoft.AspNetCore.Mvc;
using PersonnelTransport.Application.Employees.Commands.CreateEmployee;
using PersonnelTransport.Application.Employees.Queries;
using PersonnelTransport.Application.Employees.Queries.GetEmployeeById;
using Space.Abstraction;

namespace PersonnelTransport.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeesController(ISpace space) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create(CreateEmployeeCommand command, CancellationToken cancellationToken)
    {
        var id = await space.Send<Guid>(command, ct: cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id }, id);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var query = new GetEmployeeByIdQuery(id);
        var employee = await space.Send<EmployeeDto>(query, ct: cancellationToken);
        if (employee == null) return NotFound();
        return Ok(employee);
    }
}
