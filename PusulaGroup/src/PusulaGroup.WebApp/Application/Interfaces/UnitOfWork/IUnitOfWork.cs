namespace PusulaGroup.WebApp.Application.Interfaces.UnitOfWork
{
    public interface IUnitOfWork
    {
        Task SaveChangesAsync();
    }
}
