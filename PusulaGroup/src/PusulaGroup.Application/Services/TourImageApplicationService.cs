using PusulaGroup.Application.Interfaces.Repositories;
using PusulaGroup.Application.Interfaces.Services;
using PusulaGroup.Application.Interfaces.UnitOfWork;
using PusulaGroup.Domain.Entities;

namespace PusulaGroup.Application.Services
{
    public class TourImageApplicationService : BaseApplicationService<TourImage, ITourImageRepository>, ITourImageApplicationService
    {
        public TourImageApplicationService(ITourImageRepository repository, IUnitOfWork unitOfWork) : base(repository, unitOfWork)
        {
        }
    }
}
