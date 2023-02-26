namespace PusulaGroup.Infrastructure.EntityFrameworkCore.UnitOfWork
{
    public class EfCoreUnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public EfCoreUnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
