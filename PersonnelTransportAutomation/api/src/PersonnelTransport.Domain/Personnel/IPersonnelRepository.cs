namespace PersonnelTransport.Domain.Personnel;

public interface IPersonnelRepository
{
    Task<Personnel?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<List<Personnel>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Personnel personnel, CancellationToken cancellationToken);
    Task UpdateAsync(Personnel personnel, CancellationToken cancellationToken);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken);
}
