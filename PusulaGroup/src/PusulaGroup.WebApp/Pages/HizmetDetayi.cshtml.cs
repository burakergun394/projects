using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PusulaGroup.WebApp.Application.Dtos;
using PusulaGroup.WebApp.Application.Interfaces.Repositories;
using PusulaGroup.WebApp.Core.CrossCuttingConcerns.Caching;
using PusulaGroup.WebApp.Domain.Entities;

namespace PusulaGroup.WebApp.Pages
{
    public class HizmetDetayiModel : PageModel
    {
        private readonly IServiceRepository serviceRepository;
        private readonly IServiceImageRepository serviceImageRepository;
        private readonly ICache cache;

        public HizmetDetayiModel(IServiceRepository serviceRepository, IServiceImageRepository serviceImageRepository, ICache cache)
        {
            this.serviceRepository = serviceRepository;
            this.serviceImageRepository = serviceImageRepository;
            this.cache = cache;
        }

        [BindProperty(SupportsGet = true)]
        public string? Name { get; set; }

        public ServiceDetailDto Service { get; private set; }
        public int ActiveTourImageIndex { get; private set; }

        public async Task OnGet()
        {
            Service service;
            if (!cache.TryGet<Service>($"Service.ServiceDetail.GetByNameEqual{Name}", out var servicesFromCache))
            {
                service = await serviceRepository.GetAsync(x => x.Name == Name);
                cache.Add($"Services.ServiceDetail.GetByNameEqual{Name}", service, 1440);
            }
            else
            {
                service = servicesFromCache;
            }

            if (service == null)
            {
                Response.Redirect("/hizmetlerimiz");
                return;
            }

            Service = new ServiceDetailDto
            {
                Name = service.Name,
                UserFriendlyName = service.UserFriendlyName,
                Content = service.Content,
                Images = new List<ServiceImageDto>()
            };

            if (!cache.TryGet<List<ServiceImageDto>>($"Service.ServiceDetail.GetImagesByServiceIdEqual{service.Id}", out var serviceImagesFromCache))
            {
                var serviceImages = await serviceImageRepository.GetListByPredicateAsync(x => x.ServiceId == service.Id);
                foreach (var serviceImage in serviceImages)
                {
                    Service.Images.Add(new ServiceImageDto
                    {
                        Path = serviceImage.Path,
                        IsMain = serviceImage.IsMain
                    });
                }
                cache.Add($"Service.ServiceDetail.GetImagesByServiceIdEqual{service.Id}", Service.Images, 1440);
            }
            else
            {
                Service.Images.AddRange(serviceImagesFromCache);
            }

            var activeTourImageIndex = Service.Images!.FindIndex(x => x.IsMain);
            ActiveTourImageIndex = activeTourImageIndex == -1
                                    ? 0
                                    : activeTourImageIndex;

            ViewData["title"] = service.SeoTitle;
            ViewData["description"] = service.SeoDescription;
            ViewData["keywords"] = service.SeoKeywords;
            ViewData["author"] = service.SeoAuthor;
        }
    }
}
