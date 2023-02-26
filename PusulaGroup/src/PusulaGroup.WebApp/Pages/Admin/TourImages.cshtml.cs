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
    public class AdminTourImagesViewModel
    {
        public Dictionary<int, string> Tours { get; set; }
        public int SelectedTourId { get; set; }
        public int SelectedMainImageId { get; set; }
        public List<TourImage> TourImages { get; set; }
    }
    [Authorize]
    public class TourImagesModel : PageModel
    {
        private readonly ITourImageRepository tourImageRepository;
        private readonly ITourRepository tourRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly ICache cache;
        private readonly ImageHelper imageHelper;

        [BindProperty]
        public AdminTourImagesViewModel AdminTourImagesViewModel { get; set; }

        public TourImagesModel(ITourImageRepository tourImageRepository, ImageHelper imageHelper, ITourRepository tourRepository, IUnitOfWork unitOfWork, ICache cache)
        {
            this.tourImageRepository = tourImageRepository;
            this.imageHelper = imageHelper;
            this.tourRepository = tourRepository;
            this.unitOfWork = unitOfWork;
            this.cache = cache;
            AdminTourImagesViewModel = new AdminTourImagesViewModel
            {
                Tours = new Dictionary<int, string>(),
                TourImages = new List<TourImage>()
            };
        }

        public async Task OnGet()
        {
            AdminTourImagesViewModel.Tours = await GetToursAsync();

            var tourId = GetIntValueFromQuery("tourId");
            if (tourId == 0)
                return;

            AdminTourImagesViewModel.SelectedTourId = tourId;
            AdminTourImagesViewModel.TourImages = await GetTourImagesByTourIdAsync(tourId);

            var mainTourImage = AdminTourImagesViewModel.TourImages.FirstOrDefault(x => x.IsMain);
            AdminTourImagesViewModel.SelectedMainImageId = mainTourImage == null
                                                           ? 0
                                                           : mainTourImage.Id;

            ViewData["title"] = "Tour Images | Admin";

        }

        public IActionResult OnPostGetTourImages()
        {
            if (AdminTourImagesViewModel.SelectedTourId == 0)
            {
                SetErrorMessage("Please select a tour.");
                return Redirect("/admin/tourimages");
            }

            SetSuccessMessage("Tour images fetched successfully.");
            return Redirect($"/admin/tourimages?tourId={AdminTourImagesViewModel.SelectedTourId}");
        }

        public async Task<IActionResult> OnPostChooseMainImageAsync()
        {
            if (AdminTourImagesViewModel.SelectedTourId == 0)
            {
                SetErrorMessage("Please select a tour.");
                return Redirect("/admin/tourimages");
            }

            if (AdminTourImagesViewModel.SelectedMainImageId == 0)
            {
                SetErrorMessage("Please select a main tour image.");
                return Redirect("/admin/tourimages");
            }

            await UpdatePreviousMainImage(AdminTourImagesViewModel.SelectedTourId);
            await UpdateNewMainImage(AdminTourImagesViewModel.SelectedMainImageId);
            await unitOfWork.SaveChangesAsync();
            RemoveAllCache();
            SetSuccessMessage("Tour main image selected successfully.");


            return Redirect($"/admin/tourimages?tourId={AdminTourImagesViewModel.SelectedTourId}");
        }

        public async Task<IActionResult> OnPostSaveTourImagesAsync(IEnumerable<IFormFile> images)
        {
            if (AdminTourImagesViewModel.SelectedTourId == 0)
            {
                SetErrorMessage("Please select a tour.");
                return Redirect("/admin/tourimages");
            }

            if (!images.Any())
            {
                SetErrorMessage("Please upload a files.");
                return Redirect("/admin/tourimages");
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
                var path = await imageHelper.SaveImageAsync(image, "images", "tours", width: 550, height: 310);
                var tourImage = new TourImage
                {
                    TourId = AdminTourImagesViewModel.SelectedTourId,
                    IsMain = false,
                    Path = path,
                };

                await tourImageRepository.CreateAsync(tourImage);
                await unitOfWork.SaveChangesAsync();
            }
            RemoveAllCache();
            SetSuccessMessage("Tour images saved.");

            return Redirect($"/admin/tourimages?tourId={AdminTourImagesViewModel.SelectedTourId}");
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            if (id == 0)
            {
                SetErrorMessage("Deleting tour image id is not valid.");
                return Redirect($"/admin/tourimages?tourId={AdminTourImagesViewModel.SelectedTourId}");
            }

            var tourImage = await tourImageRepository.GetAsync(x => x.Id == id);
            if (tourImage == null)
            {
                SetErrorMessage("Deleting tour image is not found.");
                return Redirect($"/admin/tourimages?tourId={AdminTourImagesViewModel.SelectedTourId}");
            }

            await tourImageRepository.DeleteAsync(tourImage);
            await unitOfWork.SaveChangesAsync();
            imageHelper.DeleteImage(tourImage.Path);
            RemoveAllCache();
            SetSuccessMessage("Tour image deleted successfully.");
            return Redirect($"/admin/tourimages?tourId={AdminTourImagesViewModel.SelectedTourId}");
        }

        private async Task<Dictionary<int, string>> GetToursAsync()
        {
            var result = new Dictionary<int, string>();

            var tours = await tourRepository.GetListAsync();

            if (tours != null)
            {
                foreach (var tour in tours)
                {
                    result.Add(tour.Id, tour.UserFriendlyName);
                }
            }

            return result;
        }

        private async Task<List<TourImage>> GetTourImagesByTourIdAsync(int tourId)
        {
            return await tourImageRepository.GetListByPredicateAsync(x => x.TourId == tourId)
                ?? new List<TourImage>();
        }

        private int GetIntValueFromQuery(string queryKey)
        {
            var value = HttpContext.Request.Query[queryKey].ToString();
            if (!int.TryParse(value, out var result))
                result = 0;

            return result;
        }

        private async Task UpdatePreviousMainImage(int tourId)
        {
            var data = await tourImageRepository.GetAsync(x => x.IsMain && x.TourId == tourId);
            if (data != null)
            {
                data.IsMain = false;
                await tourImageRepository.UpdateAsync(data);
            }
        }

        private async Task UpdateNewMainImage(int id)
        {
            var data = await tourImageRepository.GetAsync(x => x.Id == id);
            if (data != null)
            {
                data.IsMain = true;
                await tourImageRepository.UpdateAsync(data);
            }
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
            cache.Remove("Index.GetTours");
            cache.Remove("Index.GetSliderTours");
            cache.Remove("Tours.GetListTourDetail");
        }
    }
}
