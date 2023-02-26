using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PusulaGroup.WebApp.Application.Interfaces.Repositories;
using PusulaGroup.WebApp.Core.CrossCuttingConcerns.Caching;
using PusulaGroup.WebApp.Core.Helpers;
using PusulaGroup.WebApp.Domain.Entities;
using PusulaGroup.WebApp.Pages.Admin;
using System.Text;

namespace PusulaGroup.WebApp.Pages
{
    public class ContactViewModel
    {
        public string Message { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }

    public class IletisimModel : PageModel
    {
        private readonly IGeneralInfoRepository generalInfoRepository;
        private readonly ICache cache;
        private readonly EmailHelper emailHelper;
        public IletisimModel(IGeneralInfoRepository generalInfoRepository, ICache cache, EmailHelper emailHelper)
        {
            this.generalInfoRepository = generalInfoRepository;
            this.cache = cache;
            this.emailHelper = emailHelper;

            ContactViewModel = new();
        }

        [BindProperty]
        public ContactViewModel ContactViewModel { get; set; }

        public async Task OnGet()
        {
            var generalInfo = await GetGeneralInfo();
            ViewData["title"] = "Ýletiþim";
            ViewData["description"] = generalInfo.SeoDescription;
            ViewData["keywords"] = generalInfo.SeoKeywords;
            ViewData["author"] = generalInfo.SeoAuthor;

            ViewData["aftermailsend"] = "";
        }

        public async Task<IActionResult> OnPostSendAsync()
        {
            StringBuilder bodyBuilder = new();
            var generalInfo = await GetGeneralInfo();
            if (IsFormFieldsUnvalid())
            {
                SetErrorMessage("Eksik bilgi girdiniz.");
                return Redirect("/iletisim");
            }
            else
            {
                bodyBuilder.AppendLine("Aþaðýdaki bilgilere sahip kiþi sizinle iletiþime geçmek istiyor.");
                bodyBuilder.AppendLine($"Ad Soyad: {ContactViewModel.Name}");
                bodyBuilder.AppendLine($"Email: {ContactViewModel.Email}");
                bodyBuilder.AppendLine($"Telefon numarasý: {ContactViewModel.PhoneNumber}");
                bodyBuilder.AppendLine($"Mesaj: {ContactViewModel.Message}");
                await emailHelper.SendEmailAsync(new SendEmailParams
                {
                    Host = generalInfo.EMailHost,
                    Port = generalInfo.EmailPort,
                    UserName = generalInfo.EmailFrom,
                    Password = generalInfo.EmailPassword,
                    To = generalInfo.Email,
                    Body = bodyBuilder.ToString(),
                    Subject = "Pusula Grup Turizm Ýletiþim Talebi"
                });

                SetSuccessMessage("Ýletiþim talebiniz iletildi.");
                return Redirect("/iletisim");
            }
        }

        private bool IsFormFieldsUnvalid() {
            return string.IsNullOrWhiteSpace(ContactViewModel.Name)
                    || string.IsNullOrWhiteSpace(ContactViewModel.Email)
                    || string.IsNullOrWhiteSpace(ContactViewModel.PhoneNumber)
                    || string.IsNullOrWhiteSpace(ContactViewModel.Message);
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

        private void SetErrorMessage(string errorMessage)
        {
            TempData["ErrorMessage"] = errorMessage;
        }

        private void SetSuccessMessage(string successMessage)
        {
            TempData["SuccessMessage"] = successMessage;
        }
    }
}
