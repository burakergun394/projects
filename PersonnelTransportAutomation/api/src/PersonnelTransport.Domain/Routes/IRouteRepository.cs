namespace PersonnelTransport.Domain.Routes;

public interface IRouteRepository
{
    Task<Route?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<List<Route>> GetActiveRoutesAsync(CancellationToken cancellationToken);
    Task AddAsync(Route route, CancellationToken cancellationToken);
    Task UpdateAsync(Route route, CancellationToken cancellationToken);
}
