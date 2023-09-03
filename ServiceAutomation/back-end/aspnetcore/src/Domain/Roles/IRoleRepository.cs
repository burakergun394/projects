using Domain.Shared;

namespace Domain.Roles;

public interface IRoleRepository: IRepository<Role, Guid>
{
    Task<bool> IsRoleExistByName(string name);
}