using PusulaGroup.Application.Interfaces.Repositories;
using PusulaGroup.Application.Interfaces.Services;
using PusulaGroup.Application.Interfaces.UnitOfWork;
using PusulaGroup.Domain.Entities;

namespace PusulaGroup.Application.Services
{
    public class ServiceApplicationService : BaseApplicationService<Service, IServiceRepository>, IServiceApplicationService
    {
        private readonly IServiceImageApplicationService serviceImageService;
        public ServiceApplicationService(IServiceRepository repository, IUnitOfWork unitOfWork, IServiceImageApplicationService serviceImageService) : base(repository, unitOfWork)
        {
            this.serviceImageService = serviceImageService;
        }
    } 
}
