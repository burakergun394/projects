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
    public class AdminServiceImagesViewModel
    {
        public Dictionary<int, string> Services { get; set; }
        public int SelectedServiceId { get; set; }
        public int SelectedMainImageId { get; set; }
        public List<ServiceImage> ServiceImages { get; set; }
    }


    [Authorize]
    public class ServiceImagesModel : PageModel
    {
        private readonly IServiceImageRepository serviceImageRepository;
        private readonly IServiceRepository serviceRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly ImageHelper imageHelper;
        private readonly ICache cache;

        [BindProperty]
        public AdminServiceImagesViewModel AdminServiceImagesViewModel { get; set; }

        public ServiceImagesModel(IServiceImageRepository serviceImageRepository, IServiceRepository serviceRepository, IUnitOfWork unitOfWork, ImageHelper imageHelper, ICache cache)
        {
            this.serviceImageRepository = serviceImageRepository;
            this.serviceRepository = serviceRepository;
            this.unitOfWork = unitOfWork;
            this.imageHelper = imageHelper;

            AdminServiceImagesViewModel = new AdminServiceImagesViewModel
            {
                Services = new Dictionary<int, string>(),
                ServiceImages = new List<ServiceImage>()
            };
            this.cache = cache;
        }

        public async Task OnGet()
        {
            AdminServiceImagesViewModel.Services = await GetServicesAsync();

            var serviceId = GetIntValueFromQuery("serviceId");
            if (serviceId == 0)
                return;

            AdminServiceImagesViewModel.SelectedServiceId = serviceId;
            AdminServiceImagesViewModel.ServiceImages = await GetServiceImagesByServiceIdAsync(serviceId);

            var mainTourImage = AdminServiceImagesViewModel.ServiceImages.FirstOrDefault(x => x.IsMain);
            AdminServiceImagesViewModel.SelectedMainImageId = mainTourImage == null
                                                          ? 0
                                                          : mainTourImage.Id;

            ViewData["title"] = "Service Images | Admin";
        }

        public IActionResult OnPostGetServiceImages()
        {
            if (AdminServiceImagesViewModel.SelectedServiceId == 0)
            {
                SetErrorMessage("Please select a service.");
                return Redirect("/admin/serviceimages");
            }

            SetSuccessMessage("Service images fetched successfully.");
            return Redirect($"/admin/serviceimages?serviceId={AdminServiceImagesViewModel.SelectedServiceId}");
        }

        public async Task<IActionResult> OnPostChooseMainImageAsync()
        {
            if (AdminServiceImagesViewModel.SelectedServiceId == 0)
            {
                SetErrorMessage("Please select a service.");
                return Redirect("/admin/tourimages");
            }

            if (AdminServiceImagesViewModel.SelectedMainImageId == 0)
            {
                SetErrorMessage("Please select a main tour image.");
                return Redirect("/admin/tourimages");
            }

            await UpdatePreviousMainImage(AdminServiceImagesViewModel.SelectedServiceId);
            await UpdateNewMainImage(AdminServiceImagesViewModel.SelectedMainImageId);
            await unitOfWork.SaveChangesAsync();
            RemoveAllCache();
            SetSuccessMessage("Service main image selected successfully.");
            return Redirect($"/admin/serviceimages?serviceId={AdminServiceImagesViewModel.SelectedServiceId}");
        }

        public async Task<IActionResult> OnPostSaveServiceImagesAsync(IEnumerable<IFormFile> images)
        {
            if (AdminServiceImagesViewModel.SelectedServiceId == 0)
            {
                SetErrorMessage("Please select a service.");
                return Redirect("/admin/serviceimages");
            }

            if (!images.Any())
            {
                SetErrorMessage("Please upload a files.");
                return Redirect("/admin/serviceimages");
            }

            foreach (var image in images)
            {
                if (!imageHelper.IsImageExtensionValid(image))
                {
                    SetErrorMessage("File extension not valid. The extension must be png, jpeg or jpg.");
                }
            }

            foreach (var image in images)
            {
                var path = await imageHelper.SaveImageAsync(image, "images", "services", width: 550, height: 310);
                var serviceImage = new ServiceImage
                {
                    ServiceId = AdminServiceImagesViewModel.SelectedServiceId,
                    IsMain = false,
                    Path = path,
                };

                await serviceImageRepository.CreateAsync(serviceImage);
                await unitOfWork.SaveChangesAsync();
            }

            SetSuccessMessage("Service images saved successfully.");
            RemoveAllCache();
            return Redirect($"/admin/serviceimages?serviceId={AdminServiceImagesViewModel.SelectedServiceId}");
        }


        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            if (id == 0)
            {
                SetErrorMessage("Deleting service image id is not valid.");
                return Redirect($"/admin/serviceimages?serviceId={AdminServiceImagesViewModel.SelectedServiceId}");
            }

            var serviceImage = await serviceImageRepository.GetAsync(x => x.Id == id);
            if (serviceImage == null)
            {
                SetErrorMessage("Deleting service image is not found.");
                return Redirect($"/admin/serviceimages?serviceId={AdminServiceImagesViewModel.SelectedServiceId}");
            }

            await serviceImageRepository.DeleteAsync(serviceImage);
            await unitOfWork.SaveChangesAsync();
            imageHelper.DeleteImage(serviceImage.Path);
            RemoveAllCache();
            SetSuccessMessage("Service image deleted successfully.");
            return Redirect($"/admin/serviceimages?serviceId={AdminServiceImagesViewModel.SelectedServiceId}");
        }


        private async Task<Dictionary<int, string>> GetServicesAsync()
        {
            var result = new Dictionary<int, string>();

            var services = await serviceRepository.GetListAsync();

            if (services != null)
            {
                foreach (var service in services)
                {
                    result.Add(service.Id, service.UserFriendlyName);
                }
            }

            return result;
        }

        private async Task<List<ServiceImage>> GetServiceImagesByServiceIdAsync(int serviceId)
        {
            return await serviceImageRepository.GetListByPredicateAsync(x => x.ServiceId == serviceId)
                ?? new List<ServiceImage>();
        }

        private async Task UpdatePreviousMainImage(int serviceId)
        {
            var data = await serviceImageRepository.GetAsync(x => x.IsMain && x.ServiceId == serviceId);
            if (data != null)
            {
                data.IsMain = false;
                await serviceImageRepository.UpdateAsync(data);
            }
        }

        private async Task UpdateNewMainImage(int id)
        {
            var data = await serviceImageRepository.GetAsync(x => x.Id == id);
            if (data != null)
            {
                data.IsMain = true;
                await serviceImageRepository.UpdateAsync(data);
            }
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
