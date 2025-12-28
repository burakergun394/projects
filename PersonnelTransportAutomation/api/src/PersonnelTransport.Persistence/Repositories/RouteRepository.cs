using Microsoft.EntityFrameworkCore;
using PersonnelTransport.Domain.Routes;

namespace PersonnelTransport.Persistence.Repositories;

public class RouteRepository(ApplicationDbContext context) : IRouteRepository
{
    private readonly ApplicationDbContext _context = context;

    public async Task AddAsync(Route route, CancellationToken cancellationToken)
    {
        await _context.Routes.AddAsync(route, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<List<Route>> GetActiveRoutesAsync(CancellationToken cancellationToken)
    {
        return await _context.Routes
            .Where(r => r.Status == RouteStatus.Active || r.Status == RouteStatus.Planning)
            .ToListAsync(cancellationToken);
    }

    public async Task<Route?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context.Routes.FindAsync([id], cancellationToken);
    }

    public async Task UpdateAsync(Route route, CancellationToken cancellationToken)
    {
        _context.Routes.Update(route);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
