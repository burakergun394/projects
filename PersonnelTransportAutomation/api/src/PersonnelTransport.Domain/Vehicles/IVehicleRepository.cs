namespace PersonnelTransport.Domain.Vehicles;

public interface IVehicleRepository
{
    Task<Vehicle?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<List<Vehicle>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Vehicle vehicle, CancellationToken cancellationToken);
    Task UpdateAsync(Vehicle vehicle, CancellationToken cancellationToken);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken);
}
