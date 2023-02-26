using Microsoft.AspNetCore.Mvc.RazorPages;
using PusulaGroup.WebApp.Application.Dtos;
using PusulaGroup.WebApp.Application.Interfaces.Repositories;
using PusulaGroup.WebApp.Core.CrossCuttingConcerns.Caching;
using PusulaGroup.WebApp.Core.Helpers;
using PusulaGroup.WebApp.Domain.Constants;
using PusulaGroup.WebApp.Domain.Entities;

namespace PusulaGroup.WebApp.Pages
{

    public class HizmetlerimizModel : PageModel
    {
        private readonly IServiceRepository serviceRepository;
        private readonly IServiceImageRepository serviceImageRepository;
        private readonly IGeneralInfoRepository generalInfoRepository;
        private readonly ICache cache;
        public HizmetlerimizModel(IServiceRepository serviceRepository, IServiceImageRepository serviceImageRepository, ICache cache, IGeneralInfoRepository generalInfoRepository)
        {
            this.serviceRepository = serviceRepository;
            this.serviceImageRepository = serviceImageRepository;
            this.cache = cache;
            this.generalInfoRepository = generalInfoRepository;
        }

        public List<AllServiceDetailDto> Services { get; set; }
        public int Division { get; private set; }
        public int NumberOfServicesInRow { get; private set; } = 3;

        public async Task OnGet()
        {
            Services = new List<AllServiceDetailDto>();

            if (!cache.TryGet<List<AllServiceDetailDto>>("Service.Services.AllServiceDetail", out var value))
            {
                var services = await serviceRepository.GetListAsync();
                foreach (var service in services)
                {
                    var serviceImage = await serviceImageRepository.GetAsync(x => x.IsMain && x.ServiceId == service.Id);
                    var imagePath = serviceImage == null
                        ? PusulaGroupConstants.ImagePathNoImage
                        : serviceImage.Path;
                    Services.Add(new AllServiceDetailDto
                    {
                        Name = service.Name,
                        UserFriendlyName = service.UserFriendlyName,
                        ImagePath = imagePath
                    });
                }
                cache.Add("Service.Services.AllServiceDetail", Services, 1440);
            }
            else
            {
                Services = value;
            }
            Division = GetDivision(Services);

            var generalInfo = await GetGeneralInfo();
            ViewData["title"] = "Hizmetlerimiz";
            ViewData["description"] = generalInfo.SeoDescription;
            ViewData["keywords"] = generalInfo.SeoKeywords;
            ViewData["author"] = generalInfo.SeoAuthor;
        }

        private async Task<GeneralInfo> GetGeneralInfo()
        {
            GeneralInfo result = new();
            if (!cache.TryGet<GeneralInfo>("GeneralInfo.Layout.Get", out var value))
            {
                result = await generalInfoRepository.AnyAsync(x => x.Id > 0)
                ? (await generalInfoRepository.GetListAsync()).First()
                : new GeneralInfo();
                cache.Add("GeneralInfo.Layout.Get", result, 1440);
            }
            else
            {
                result = value;
            }
            return result;
        }

        private int GetDivision(List<AllServiceDetailDto> services)
        {
            return services.Count / NumberOfServicesInRow;
        }
    }
}
