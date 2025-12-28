using Microsoft.EntityFrameworkCore;
using PersonnelTransport.Domain.Employees;

namespace PersonnelTransport.Persistence.Repositories;

public class EmployeeRepository(ApplicationDbContext context) : IEmployeeRepository
{
    private readonly ApplicationDbContext _context = context;

    public async Task AddAsync(Employee employee, CancellationToken cancellationToken)
    {
        await _context.Employees.AddAsync(employee, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var entity = await _context.Employees.FindAsync([id], cancellationToken);
        if (entity != null)
        {
            _context.Employees.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }

    public async Task<List<Employee>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _context.Employees.ToListAsync(cancellationToken);
    }

    public async Task<Employee?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context.Employees.FindAsync([id], cancellationToken);
    }

    public async Task UpdateAsync(Employee employee, CancellationToken cancellationToken)
    {
        _context.Employees.Update(employee);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
