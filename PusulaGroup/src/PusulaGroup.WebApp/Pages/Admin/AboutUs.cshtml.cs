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
    public class AdminAboutUsViewModel
    {
        public AboutUs AboutUs { get; set; }
    }
    [Authorize]
    public class AboutUsModel : PageModel
    {
        private readonly IAboutUsRepository aboutUsRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly ImageHelper imageHelper;
        private readonly ICache cache;

        [BindProperty]
        public AdminAboutUsViewModel AdminAboutUsViewModel { get; set; }

        public AboutUsModel(IAboutUsRepository aboutUsRepository, IUnitOfWork unitOfWork, ImageHelper imageHelper, ICache cache)
        {
            this.aboutUsRepository = aboutUsRepository;
            this.unitOfWork = unitOfWork;
            this.imageHelper = imageHelper;

            AdminAboutUsViewModel = new AdminAboutUsViewModel
            {
                AboutUs = new AboutUs()
            };
            this.cache = cache;
        }

        public async Task OnGet()
        {
            AdminAboutUsViewModel.AboutUs = await GetAboutUsAsync();

            ViewData["title"] = "About Us | Admin";
        }

        public async Task<IActionResult> OnPostSaveAsync(IFormFile image)
        {
            var aboutUs = await GetAboutUsAsync();

            aboutUs.AboutUsContent = AdminAboutUsViewModel.AboutUs.AboutUsContent;
            aboutUs.OurVisionContent = AdminAboutUsViewModel.AboutUs.OurVisionContent;
            aboutUs.SeoTitle = AdminAboutUsViewModel.AboutUs.SeoTitle ?? "";
            aboutUs.SeoDescription = AdminAboutUsViewModel.AboutUs.SeoDescription ?? "";
            aboutUs.SeoKeywords = AdminAboutUsViewModel.AboutUs.SeoKeywords ?? "";
            aboutUs.SeoAuthor = AdminAboutUsViewModel.AboutUs.SeoAuthor ?? "";

            if (image != null && image.Length > 0)
            {
                if (!imageHelper.IsImageExtensionValid(image))
                {
                    SetErrorMessage("File extension not valid. The extension must be png, jpeg or jpg.");
                }
                else
                {
                    var path = await imageHelper.SaveImageAsync(image, "images", "aboutus", width: 850, height: 700);
                    imageHelper.DeleteImage(aboutUs.ImagePath);
                    aboutUs.ImagePath = path;
                }
            }

            if (aboutUs.Id == 0)
            {
                await aboutUsRepository.CreateAsync(aboutUs);
            }
            else
            {
                await aboutUsRepository.UpdateAsync(aboutUs);
            }

            await unitOfWork.SaveChangesAsync();
            RemoveAllCache();
            SetSuccessMessage("Saved successfully.");
            return Redirect("/admin/aboutus");
        }

        private async Task<AboutUs> GetAboutUsAsync()
        {
            var aboutUses = await aboutUsRepository.GetListAsync();
            return aboutUses == null || !aboutUses.Any() ? new AboutUs() : aboutUses.First(); 
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
            cache.RemoveKeysByStartingValue("About.");

        }
    }
}
