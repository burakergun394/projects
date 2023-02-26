using PusulaGroup.Domain.Entities;
using System.Linq.Expressions;

namespace PusulaGroup.Application.Interfaces.Services
{
    public interface IServiceImageApplicationService : IBaseApplicationService<ServiceImage>
    {
        Task<ServiceImage> GetByPredicateAndSetNoImagePathIfImageNotFoundAsync(Expression<Func<ServiceImage, bool>> predicate);
        Task<List<ServiceImage>> GetListByPredicateAndSetNoImagePathIfImageNotFoundAsync(Expression<Func<ServiceImage, bool>> predicate);
    }
}
