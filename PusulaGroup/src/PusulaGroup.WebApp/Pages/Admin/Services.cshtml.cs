using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PusulaGroup.WebApp.Application.Interfaces.Repositories;
using PusulaGroup.WebApp.Application.Interfaces.UnitOfWork;
using PusulaGroup.WebApp.Core.CrossCuttingConcerns.Caching;
using PusulaGroup.WebApp.Core.Helpers;
using PusulaGroup.WebApp.Domain.Entities;

namespace PusulaGroup.WebApp.Pages.Admin
{
    public class AdminServiceDetail
    {
        public int ServiceId { get; set; }
        public string UserFriendlyName { get; set; }
    }

    public class AdminServicesViewModel
    {
        public Service Service { get; set; }
        public List<AdminServiceDetail> Services { get; set; }
    }

    [Authorize]
    public class ServicesModel : PageModel
    {
        private readonly IServiceRepository serviceRepository;
        private readonly IServiceImageRepository serviceImageRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly StringHelper stringHelper;
        private readonly ImageHelper imageHelper;
        private readonly ICache cache;

        [BindProperty]
        public AdminServicesViewModel AdminServicesViewModel { get; set; }

        public ServicesModel(IServiceRepository serviceRepository, IServiceImageRepository serviceImageRepository, IUnitOfWork unitOfWork, StringHelper stringHelper, ImageHelper imageHelper, ICache cache)
        {
            this.serviceRepository = serviceRepository;
            this.serviceImageRepository = serviceImageRepository;
            this.unitOfWork = unitOfWork;
            this.stringHelper = stringHelper;
            this.imageHelper = imageHelper;

            AdminServicesViewModel = new AdminServicesViewModel
            {
                Service = new Service(),
                Services = new List<AdminServiceDetail>()
            };
            this.cache = cache;
        }

        public async Task OnGet()
        {
            AdminServicesViewModel.Services.AddRange(await GetServicesAsync());

            var serviceId = GetIntValueFromQuery("id");
            if (serviceId == 0)
                return;

            AdminServicesViewModel.Service = await GetServiceByServiceId(serviceId);

            ViewData["title"] = "Services | Admin";

        }

        public async Task<IActionResult> OnPostInsertOrUpdateAsync(int id)
        {
            var service = await serviceRepository.GetAsync(x => x.Id == id);
            if (service == null)
                service = new Service();

            var name = stringHelper.TurkishCharacterToEnglish(AdminServicesViewModel.Service.UserFriendlyName);
            var isExist = await serviceRepository.AnyAsync(x => x.Name == name);

            if (isExist && id == 0)
            {
                SetErrorMessage("You have already added a record with the service name you specified. You cannot add again.");
                return Redirect("/admin/services");
            }

            service.Name = stringHelper.TurkishCharacterToEnglish(AdminServicesViewModel.Service.UserFriendlyName);
            service.UserFriendlyName = AdminServicesViewModel.Service.UserFriendlyName;
            service.Content = AdminServicesViewModel.Service.Content;
            service.SeoTitle = AdminServicesViewModel.Service.SeoTitle ?? "";
            service.SeoDescription = AdminServicesViewModel.Service.SeoDescription ?? "";
            service.SeoKeywords = AdminServicesViewModel.Service.SeoKeywords ?? "";
            service.SeoAuthor = AdminServicesViewModel.Service.SeoAuthor ?? "";

            if (service.Id == 0)
            {
                await serviceRepository.CreateAsync(service);
            }
            else
            {
                await serviceRepository.UpdateAsync(service);
            }

            await unitOfWork.SaveChangesAsync();
            RemoveAllCache();
            SetSuccessMessage("Saved successfully.");
            return Redirect("/admin/services");
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            if (id == 0)
            {
                SetErrorMessage("Id cannot be 0.");
                return Redirect("/admin/services");
            }

            var service = await serviceRepository.GetAsync(x => x.Id == id);
            if (service == null)
            {
                SetErrorMessage("Service not found.");
                return RedirectToPagePermanent("/admin/service");
            }

            await serviceRepository.DeleteAsync(service);

            var serviceImages = await serviceImageRepository.GetListByPredicateAsync(x => x.ServiceId == id);
            if (serviceImages != null)
            {
                foreach (var serviceImage in serviceImages)
                {
                    await serviceImageRepository.DeleteAsync(serviceImage);
                    imageHelper.DeleteImage(serviceImage.Path);
                }
            }

            await unitOfWork.SaveChangesAsync();
            RemoveAllCache();
            SetSuccessMessage("Deleted successfully.");
            return RedirectToPagePermanent("/admin/tours");
        }

        private async Task<List<AdminServiceDetail>> GetServicesAsync()
        {
            var result = new List<AdminServiceDetail>();
            var services = await serviceRepository.GetListAsync();
            if (services != null && services.Any())
            {
                foreach (var service in services)
                {
                    result.Add(new AdminServiceDetail
                    {
                        ServiceId = service.Id,
                        UserFriendlyName = service.UserFriendlyName,
                    });
                }
            }

            return result;
        }

        private async Task<Service> GetServiceByServiceId(int id)
        {
            var service = await serviceRepository.GetAsync(x => x.Id == id);
            return service ?? new Service();
        }

        private int GetIntValueFromQuery(string queryKey)
        {
            var value = HttpContext.Request.Query[queryKey].ToString();
            if (!int.TryParse(value, out var result))
                result = 0;

            return result;
        }

        private void SetErrorMessage(string errorMessage)
        {
            TempData["ErrorMessage"] = errorMessage;
        }

        private void SetSuccessMessage(string successMessage)
        {
            TempData["SuccessMessage"] = successMessage;
        }

        private void RemoveAllCache()
        {
            cache.RemoveKeysByStartingValue("Service.");
        }
    }
}
