namespace PersonnelTransport.Persistence.Repositories;

using Microsoft.EntityFrameworkCore;
using PersonnelTransport.Domain.Personnel;

public class PersonnelRepository(ApplicationDbContext context) : IPersonnelRepository
{
    private readonly ApplicationDbContext _context = context;

    public async Task AddAsync(Personnel personnel, CancellationToken cancellationToken)
    {
        await _context.Personnel.AddAsync(personnel, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var entity = await _context.Personnel.FindAsync([id], cancellationToken);
        if (entity != null)
        {
            _context.Personnel.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }

    public async Task<List<Personnel>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _context.Personnel.ToListAsync(cancellationToken);
    }

    public async Task<Personnel?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context.Personnel.FindAsync([id], cancellationToken);
    }

    public async Task UpdateAsync(Personnel personnel, CancellationToken cancellationToken)
    {
        _context.Personnel.Update(personnel);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
