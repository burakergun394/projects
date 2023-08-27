using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PusulaGroup.WebApp.Application.Dtos;
using PusulaGroup.WebApp.Application.Interfaces.Repositories;
using PusulaGroup.WebApp.Application.Interfaces.UnitOfWork;
using PusulaGroup.WebApp.Core.CrossCuttingConcerns.Caching;
using PusulaGroup.WebApp.Core.Helpers;
using PusulaGroup.WebApp.Domain.Entities;

namespace PusulaGroup.WebApp.Pages.Admin
{
    public class AdminTourDetail
    {
        public int TourId { get; set; }
        public string UserFriendlyName { get; set; }
        public string MainTourName { get; set; }
    }

    public class AdminToursViewModel
    {
        public Tour Tour { get; set; }
        public List<AdminTourDetail> Tours { get; set; }
    }

    [Authorize]
    public class ToursModel : PageModel
    {

        private readonly ITourRepository tourRepository;
        private readonly ITourImageRepository tourImageRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly StringHelper stringHelper;
        private readonly ImageHelper imageHelper;
        private readonly ICache cache;

        [BindProperty]
        public AdminToursViewModel AdminToursViewModel { get; set; }

        public ToursModel(ITourRepository tourRepository, ITourImageRepository tourImageRepository, IUnitOfWork unitOfWork, StringHelper stringHelper, ImageHelper imageHelper, ICache cache)
        {
            this.tourRepository = tourRepository;
            this.tourImageRepository = tourImageRepository;
            this.unitOfWork = unitOfWork;
            this.stringHelper = stringHelper;
            this.imageHelper = imageHelper;

            AdminToursViewModel = new AdminToursViewModel
            {
                Tour = new Tour(),
                Tours = new List<AdminTourDetail>()
            };
            this.cache = cache;
        }
        public async Task OnGet()
        {
            AdminToursViewModel.Tours.AddRange(await GetToursAsync());
            
            var tourId = GetIntValueFromQuery("id");
            if (tourId == 0)
                return;

            AdminToursViewModel.Tour = await GetTourByTourId(tourId);

            ViewData["title"] = "Tours | Admin";
        }

        public async Task<IActionResult> OnPostInsertOrUpdateAsync(int id)
        {
            var tour = await tourRepository.GetAsync(x => x.Id == id);
            if (tour == null)
                tour = new Tour();

            var name = stringHelper.TurkishCharacterToEnglish(AdminToursViewModel.Tour.UserFriendlyName);
            var isExist = await tourRepository.AnyAsync(x => x.Name == name);

            if (isExist && id == 0)
            {
                SetErrorMessage("You have already added a record with the tour name you specified. You cannot add again.");
                return Redirect("/admin/tours");
            }

            tour.Name = stringHelper.TurkishCharacterToEnglish(AdminToursViewModel.Tour.UserFriendlyName);
            tour.UserFriendlyName = AdminToursViewModel.Tour.UserFriendlyName;
            tour.ParentId = AdminToursViewModel.Tour.ParentId;
            tour.Content = AdminToursViewModel.Tour.Content;
            tour.SliderText = AdminToursViewModel.Tour.SliderText;
            tour.IsShowSlider = AdminToursViewModel.Tour.IsShowSlider;
            tour.SliderOrderIndex = AdminToursViewModel.Tour.SliderOrderIndex;
            tour.SeoTitle = AdminToursViewModel.Tour.SeoTitle ?? "";
            tour.SeoDescription = AdminToursViewModel.Tour.SeoDescription ?? "";
            tour.SeoKeywords = AdminToursViewModel.Tour.SeoKeywords ?? "";
            tour.SeoAuthor = AdminToursViewModel.Tour.SeoAuthor ?? "";

            if (tour.Id == 0)
            {
                await tourRepository.CreateAsync(tour);
            }
            else
            {
                await tourRepository.UpdateAsync(tour);
            }

            await unitOfWork.SaveChangesAsync();
            RemoveAllCache();
            SetSuccessMessage("Saved successfully.");
            return Redirect("/admin/tours");
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            if (id == 0)
            {
                SetErrorMessage("Id cannot be 0.");
                return Redirect("/admin/tours");
            }

            var tour = await tourRepository.GetAsync(x => x.Id == id);
            if (tour == null)
            {
                SetErrorMessage("Tour not found.");
                return RedirectToPagePermanent("/admin/tours");
            }

            await tourRepository.DeleteAsync(tour);

            var childTours = await tourRepository.GetListByPredicateAsync(x => x.ParentId == tour.Id);
            if (childTours != null)
            {
                foreach (var childTour in childTours)
                {
                    childTour.ParentId = 0;
                    await tourRepository.UpdateAsync(childTour);
                }
            }

            var tourImages = await tourImageRepository.GetListByPredicateAsync(x => x.TourId == id);
            if (tourImages != null)
            {
                foreach (var tourImage in tourImages)
                {
                    await tourImageRepository.DeleteAsync(tourImage);
                    imageHelper.DeleteImage(tourImage.Path);
                }
            }

            await unitOfWork.SaveChangesAsync();
            RemoveAllCache();
            SetSuccessMessage("Deleted successfully.");
            return RedirectToPagePermanent("/admin/tours");
        }

        private async Task<List<AdminTourDetail>> GetToursAsync()
        {
            var result = new List<AdminTourDetail>();
            var tours = await tourRepository.GetListAsync();
            if (tours != null && tours.Any())
            {
                foreach (var tour in tours)
                {
                    var mainTourName = string.Empty;
                    if (tour.ParentId == 0)
                        mainTourName = "Main Tour";
                    else
                    {
                        var parentTour = await tourRepository.GetAsync(x => x.Id == tour.ParentId);
                        if (parentTour == null)
                            mainTourName = "Main Tour Not Found";
                        else
                            mainTourName = parentTour.UserFriendlyName;
                    }

                    result.Add(new AdminTourDetail
                    {
                        TourId = tour.Id,
                        UserFriendlyName = tour.UserFriendlyName,
                        MainTourName = mainTourName
                    });
                }
            }

            return result;
        }

        private async Task<Tour> GetTourByTourId(int id)
        {
            var tour = await tourRepository.GetAsync(x => x.Id == id);
            return tour ?? new Tour();
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
            cache.RemoveKeysByStartingValue("Tours.");
        }
    }
}
