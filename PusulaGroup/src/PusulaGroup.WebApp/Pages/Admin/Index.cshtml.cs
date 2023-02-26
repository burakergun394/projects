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
    public class AdminIndexViewModel
    {
        public GeneralInfo GeneralInfo { get; set; }
    }

    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly IGeneralInfoRepository generalInfoRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly ImageHelper imageHelper;
        private readonly ICache cache;

        [BindProperty]
        public AdminIndexViewModel AdminIndexViewModel { get; set; }

        public IndexModel(IGeneralInfoRepository generalInfoRepository, IUnitOfWork unitOfWork, ImageHelper imageHelper, ICache cache)
        {
            this.generalInfoRepository = generalInfoRepository;
            this.unitOfWork = unitOfWork;
            this.imageHelper = imageHelper;
            this.cache = cache;

            AdminIndexViewModel = new AdminIndexViewModel
            {
                GeneralInfo = new GeneralInfo()
            };
        }

        public async Task OnGet()
        {
            AdminIndexViewModel.GeneralInfo = await GetGeneralInfoAsync();
            ViewData["title"] = "Index | Admin";
        }

        public async Task<IActionResult> OnPostSaveAsync(IFormFile image)
        {
            var generalInfo = await GetGeneralInfoAsync();

            generalInfo.Email = AdminIndexViewModel.GeneralInfo.Email;
            generalInfo.EMailHost = AdminIndexViewModel.GeneralInfo.EMailHost;
            generalInfo.EmailFrom = AdminIndexViewModel.GeneralInfo.EmailFrom;
            generalInfo.EmailPort = AdminIndexViewModel.GeneralInfo.EmailPort;
            generalInfo.EmailPassword = AdminIndexViewModel.GeneralInfo.EmailPassword;
            generalInfo.Address1 = AdminIndexViewModel.GeneralInfo.Address1 ?? "";
            generalInfo.Address2 = AdminIndexViewModel.GeneralInfo.Address2 ?? "";
            generalInfo.FacebookUrl = AdminIndexViewModel.GeneralInfo.FacebookUrl ?? "";
            generalInfo.InstagramUrl = AdminIndexViewModel.GeneralInfo.InstagramUrl ?? "";
            generalInfo.TwitterUrl = AdminIndexViewModel.GeneralInfo.TwitterUrl ?? "";
            generalInfo.LinkedinUrl = AdminIndexViewModel.GeneralInfo.LinkedinUrl ?? "";
            generalInfo.TelephoneNumber = AdminIndexViewModel.GeneralInfo.TelephoneNumber ?? "";
            generalInfo.WhatsappTelephoneNumber = AdminIndexViewModel.GeneralInfo.WhatsappTelephoneNumber ?? "";
            generalInfo.SeoTitle = AdminIndexViewModel.GeneralInfo.SeoTitle ?? "";
            generalInfo.SeoDescription = AdminIndexViewModel.GeneralInfo.SeoDescription ?? "";
            generalInfo.SeoKeywords = AdminIndexViewModel.GeneralInfo.SeoKeywords ?? "";
            generalInfo.SeoAuthor = AdminIndexViewModel.GeneralInfo.SeoAuthor ?? "";

            if (image != null && image.Length > 0)
            {
                if (!imageHelper.IsImageExtensionValid(image))
                {
                    SetErrorMessage("File extension not valid. The extension must be png, jpeg or jpg.");
                }
                else
                {
                    var path = await imageHelper.SaveImageAsync(image, "images", "general", width: 150, height: 200);
                    imageHelper.DeleteImage(generalInfo.LogoPath);
                    generalInfo.LogoPath = path;
                }
            }

            if (generalInfo.Id == 0)
            {
                await generalInfoRepository.CreateAsync(generalInfo);
            }
            else
            {
                await generalInfoRepository.UpdateAsync(generalInfo);
            }
            await unitOfWork.SaveChangesAsync();
            RemoveAllCache();
            SetSuccessMessage("Saved successfully.");
            return Redirect("/admin/index");
        }

        private async Task<GeneralInfo> GetGeneralInfoAsync()
        {
            var generalInfos = await generalInfoRepository.GetListAsync();
            return generalInfos == null || !generalInfos.Any() ? new GeneralInfo() : generalInfos.First();
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
            cache.RemoveKeysByStartingValue("GeneralInfo.");
        }
    }
}
