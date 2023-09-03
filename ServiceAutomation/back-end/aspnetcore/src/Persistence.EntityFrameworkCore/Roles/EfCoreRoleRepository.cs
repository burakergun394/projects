using Domain.Roles;
using Microsoft.EntityFrameworkCore;
using Persistence.EntityFrameworkCore.Shared;

namespace Persistence.EntityFrameworkCore.Roles;

internal class EfCoreRoleRepository : EfCoreRepository<Role, Guid, ApplicationDbContext>, IRoleRepository
{
    public EfCoreRoleRepository(ApplicationDbContext context) : base(context)
    {
         
    }

    public async Task<bool> IsRoleExistByName(string name) => await DbSet.AnyAsync(x => x.NormalizedName == name.ToUpperInvariant());
}