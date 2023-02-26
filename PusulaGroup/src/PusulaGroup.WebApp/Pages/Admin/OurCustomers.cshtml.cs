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
    public class AdminOurCustomerViewModel
    {
        public OurCustomer OurCustomer { get; set; }
        public List<OurCustomer> OurCustomers { get; set; }
    }

    [Authorize]
    public class OurCustomersModel : PageModel
    {
        private readonly IOurCustomerRepository ourCustomerRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly ImageHelper imageHelper;
        private readonly ICache cache;

        [BindProperty]
        public AdminOurCustomerViewModel AdminOurCustomerViewModel { get; set; }

        public OurCustomersModel(IOurCustomerRepository ourCustomerRepository, IUnitOfWork unitOfWork, ImageHelper imageHelper, ICache cache)
        {
            this.ourCustomerRepository = ourCustomerRepository;
            this.unitOfWork = unitOfWork;
            this.imageHelper = imageHelper;
            this.cache = cache;

            AdminOurCustomerViewModel = new AdminOurCustomerViewModel
            {
                OurCustomer = new OurCustomer(),
                OurCustomers = new List<OurCustomer>()
            };
        }

        public async Task OnGet()
        {
            AdminOurCustomerViewModel.OurCustomers.AddRange(await GetOurCustomersAsync());

            var ourCustomerId = GetIntValueFromQuery("id");
            if (ourCustomerId == 0)
                return;

            AdminOurCustomerViewModel.OurCustomer = await GetOurCustomerByOurCustomerId(ourCustomerId);

            ViewData["title"] = "Our Customers | Admin";
        }

        public async Task<IActionResult> OnPostInsertOrUpdateAsync(int id, IFormFile image)
        {
            var ourCustomer = await ourCustomerRepository.GetAsync(x => x.Id == id);
            if (ourCustomer == null)
                ourCustomer = new OurCustomer();

            ourCustomer.Content = AdminOurCustomerViewModel.OurCustomer.Content ?? string.Empty;
            ourCustomer.Header = AdminOurCustomerViewModel.OurCustomer.Header ?? string.Empty;
            ourCustomer.SubHeader = AdminOurCustomerViewModel.OurCustomer.SubHeader ?? string.Empty;

            if (image != null && image.Length > 0)
            {
                if (!imageHelper.IsImageExtensionValid(image))
                {
                    SetErrorMessage("File extension not valid. The extension must be png, jpeg or jpg.");
                }
                else
                {
                    var path = await imageHelper.SaveImageAsync(image, "images", "ourcustomers", width: 170, height: 170);
                    imageHelper.DeleteImage(ourCustomer.ImagePath);
                    ourCustomer.ImagePath = path;
                }
            }


            if (ourCustomer.Id == 0)
            {
                await ourCustomerRepository.CreateAsync(ourCustomer);
            }
            else
            {
                await ourCustomerRepository.UpdateAsync(ourCustomer);
            }

            await unitOfWork.SaveChangesAsync();

            RemoveAllCache();
            SetSuccessMessage("Saved successfully.");
            return Redirect("/admin/ourcustomers");
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            if (id == 0)
            {
                SetErrorMessage("Id cannot be 0.");
                return Redirect("/admin/ourcustomers");
            }

            var ourCustomer = await ourCustomerRepository.GetAsync(x => x.Id == id);
            if (ourCustomer == null)
            {
                SetErrorMessage("Our customer not found.");
                return RedirectToPagePermanent("/admin/ourcustomers");
            }

            await ourCustomerRepository.DeleteAsync(ourCustomer);
            imageHelper.DeleteImage(ourCustomer.ImagePath);

            await unitOfWork.SaveChangesAsync();

            RemoveAllCache();
            SetSuccessMessage("Deleted successfully.");
            return RedirectToPagePermanent("/admin/ourcustomers");
        }

        private async Task<List<OurCustomer>> GetOurCustomersAsync()
        {
            var result = new List<OurCustomer>();
            var ourCustomers = await ourCustomerRepository.GetListAsync();
            if (ourCustomers != null && ourCustomers.Any())
            {
                foreach (var ourCustomer in ourCustomers)
                {
                    result.Add(new OurCustomer
                    {
                        Id = ourCustomer.Id,
                        Header = ourCustomer.Header,
                        SubHeader = ourCustomer.SubHeader,
                        ImagePath = ourCustomer.ImagePath
                    });
                }
            }

            return result;
        }

        private async Task<OurCustomer> GetOurCustomerByOurCustomerId(int id)
        {
            var ourCustomer = await ourCustomerRepository.GetAsync(x => x.Id == id);
            return ourCustomer ?? new OurCustomer();
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
            cache.RemoveKeysByStartingValue("OurCustomer.");
        }
    }
}
