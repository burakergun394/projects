using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PusulaGroup.WebApp.Application.Interfaces.Repositories;
using PusulaGroup.WebApp.Core.CrossCuttingConcerns.Caching;
using PusulaGroup.WebApp.Domain.Entities;

namespace PusulaGroup.WebApp.Pages
{
    public class AboutUsViewModel
    {
        public AboutUs AboutUs { get; set; }
    }

    public class HakkimizdaModel : PageModel
    {
        private readonly IAboutUsRepository aboutUsRepository;
        private readonly ICache cache;

        [BindProperty]
        public AboutUsViewModel AboutUsViewModel { get; set; }

        public HakkimizdaModel(IAboutUsRepository aboutUsRepository, ICache cache)
        {
            this.aboutUsRepository = aboutUsRepository;

            AboutUsViewModel = new AboutUsViewModel
            {
                AboutUs = new AboutUs()
            };
            this.cache = cache;
        }

        public async Task OnGet()
        {
            var aboutUs = await GetAboutUsAsync();

            AboutUsViewModel.AboutUs = aboutUs;

            ViewData["title"] = aboutUs.SeoTitle;
            ViewData["description"] = aboutUs.SeoDescription;
            ViewData["keywords"] = aboutUs.SeoKeywords;
            ViewData["author"] = aboutUs.SeoAuthor;
        }

        private async Task<AboutUs> GetAboutUsAsync()
        {
            AboutUs result;
            if (!cache.TryGet<AboutUs>("About.About.GetAboutUs", out var value))
            {
                var aboutUs = await aboutUsRepository.GetAsync(x => x.Id > 0);
                result = aboutUs ?? new AboutUs();
                cache.Add("About.About.GetAboutUs", result, 1440);
            }
            else
            {
                result = value;
            }
            return result;
        }
    }
}
