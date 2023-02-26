using PusulaGroup.Application.Interfaces.Repositories;
using PusulaGroup.Application.Interfaces.Services;
using PusulaGroup.Application.Interfaces.UnitOfWork;
using PusulaGroup.Domain.Entities;

namespace PusulaGroup.Application.Services
{
    public class TourApplicationService : BaseApplicationService<Tour, ITourRepository>, ITourApplicationService
    {
        public TourApplicationService(ITourRepository repository, IUnitOfWork unitOfWork) : base(repository, unitOfWork)
        {
        }
    }
}
