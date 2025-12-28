namespace PersonnelTransport.Persistence.Repositories;

using Microsoft.EntityFrameworkCore;
using PersonnelTransport.Domain.Vehicles;

public class VehicleRepository(ApplicationDbContext context) : IVehicleRepository
{
    private readonly ApplicationDbContext _context = context;

    public async Task AddAsync(Vehicle vehicle, CancellationToken cancellationToken)
    {
        await _context.Vehicles.AddAsync(vehicle, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var entity = await _context.Vehicles.FindAsync([id], cancellationToken);
        if (entity != null)
        {
            _context.Vehicles.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }

    public async Task<List<Vehicle>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _context.Vehicles.ToListAsync(cancellationToken);
    }

    public async Task<Vehicle?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context.Vehicles.FindAsync([id], cancellationToken);
    }

    public async Task UpdateAsync(Vehicle vehicle, CancellationToken cancellationToken)
    {
        _context.Vehicles.Update(vehicle);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
