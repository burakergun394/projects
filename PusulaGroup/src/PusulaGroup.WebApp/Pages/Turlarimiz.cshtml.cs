using Microsoft.AspNetCore.Mvc.RazorPages;
using PusulaGroup.WebApp.Application.Dtos;
using PusulaGroup.WebApp.Application.Interfaces.Repositories;
using PusulaGroup.WebApp.Core.CrossCuttingConcerns.Caching;
using PusulaGroup.WebApp.Domain.Constants;
using PusulaGroup.WebApp.Domain.Entities;

namespace PusulaGroup.WebApp.Pages
{
    public class TurlarimizModel : PageModel
    {
        private readonly ITourRepository tourRepository;
        private readonly ITourImageRepository tourImageRepository;
        private readonly IGeneralInfoRepository generalInfoRepository;
        private readonly ICache cache;
        public TurlarimizModel(ITourRepository tourRepository, ITourImageRepository tourImageRepository, ICache cache, IGeneralInfoRepository generalInfoRepository)
        {
            this.tourRepository = tourRepository;
            this.tourImageRepository = tourImageRepository;

            Tours = new();
            this.cache = cache;
            this.generalInfoRepository = generalInfoRepository;
        }

        public List<GetListTourDetailDto> Tours { get; private set; }
        public int Division { get; private set; }
        public int NumberOfToursInRow { get; private set; } = 3;
        public async Task OnGet()
        {
            if (!cache.TryGet<List<GetListTourDetailDto>>("Tours.GetListTourDetail", out var value))
            {
                var tours = await tourRepository.GetListByPredicateAsync(x => x.ParentId == 0);

                foreach (var tour in tours)
                {
                    var tourImage = await tourImageRepository.GetAsync(x => x.IsMain && x.TourId == tour.Id);
                    var imagePath = tourImage == null
                        ? PusulaGroupConstants.ImagePathNoImage
                        : tourImage.Path;
                    Tours.Add(new GetListTourDetailDto
                    {
                        Name = tour.Name,
                        UserFriendlyName = tour.UserFriendlyName,
                        ImagePath = imagePath
                    });
                }
                cache.Add("Tours.GetListTourDetail", Tours, 1440);
            }
            else
            {
                Tours = value;
            }

            Division = GetDivision(Tours);

            var generalInfo = await GetGeneralInfo();
            ViewData["title"] = "Turlarýmýz";
            ViewData["description"] = generalInfo.SeoDescription;
            ViewData["keywords"] = generalInfo.SeoKeywords;
            ViewData["author"] = generalInfo.SeoAuthor;
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

        private int GetDivision(List<GetListTourDetailDto> tours)
        {
            return tours.Count / NumberOfToursInRow;
        }
    }
}
