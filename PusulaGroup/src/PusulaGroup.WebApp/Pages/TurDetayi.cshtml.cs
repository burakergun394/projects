using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PusulaGroup.WebApp.Application.Dtos;
using PusulaGroup.WebApp.Application.Interfaces.Repositories;
using PusulaGroup.WebApp.Core.CrossCuttingConcerns.Caching;
using PusulaGroup.WebApp.Domain.Constants;
using PusulaGroup.WebApp.Domain.Entities;

namespace PusulaGroup.WebApp.Pages
{
    public class TurDetayiModel : PageModel
    {
        private readonly ITourRepository tourRepository;
        private readonly ITourImageRepository tourImageRepository;
        private readonly ICache cache;
        public TurDetayiModel(ITourRepository tourRepository, ITourImageRepository tourImageRepository, ICache cache)
        {
            this.tourRepository = tourRepository;
            this.tourImageRepository = tourImageRepository;

            TourDetails = new GetTourDetailDto
            {
                TourImages = new(),
                SubTours = new()
            };

            ActiveTourImageIndex = 0;
            this.cache = cache;
        }

        [BindProperty(SupportsGet = true)]
        public string? Name { get; set; }

        public GetTourDetailDto TourDetails { get; private set; }

        public int ActiveTourImageIndex { get; private set; }

        public async Task OnGet()
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                Response.Redirect("/turlarimiz");
                return;
            }

            Tour mainTour = null;

            if (!cache.TryGet<Tour>($"Tour.TourDetails.TourNameEqual{Name}", out var tourFromCache))
            {
                var tourFromDb = await tourRepository.GetAsync(x => x.Name == Name.ToLower());
                if (tourFromDb != null)
                {
                    mainTour = tourFromDb;
                    cache.Add($"Tour.TourDetails.TourNameEqual{Name}", tourFromDb, 1440);
                }
            }
            else
            {
                mainTour = tourFromCache;
            }

            if (mainTour == null)
            {
                Response.Redirect("/turlarimiz");
                return;
            }

            TourDetails.UserFriendlyName = mainTour.UserFriendlyName;
            TourDetails.Content = mainTour.Content;

            List<Tour> subTours = null;

            if (!cache.TryGet<List<Tour>>($"Tour.TourDetails.ParentIdEqual{mainTour.Id}", out var subToursFromCache))
            {
                var subToursDb = await tourRepository.GetListByPredicateAsync(x => x.ParentId == mainTour.Id);
                if (subToursDb != null)
                {
                    subTours = subToursDb;
                    cache.Add($"Tour.TourDetails.ParentIdEqual{mainTour.Id}", subToursDb, 1440);
                }
            }
            else
            {
                subTours = subToursFromCache;
            }

            if (subTours == null || !subTours.Any())
                await SetPropertiesForMainToursAsync(mainTour);
            else
                await SetPropertiesForSubToursAsync(subTours);

            ViewData["title"] = mainTour.SeoTitle;
            ViewData["description"] = mainTour.SeoDescription;
            ViewData["keywords"] = mainTour.SeoKeywords;
            ViewData["author"] = mainTour.SeoAuthor;
        }

        private async Task SetPropertiesForMainToursAsync(Tour mainTour)
        {
            TourDetails.IsMainTour = true;

            List<TourImage> mainTourImages = null;
            if (!cache.TryGet<List<TourImage>>($"Tour.TourDetails.GetTourImagesByTourIdEqual{mainTour.Id}", out var mainTourImagesFromCache))
            {
                var mainTourImagesDb = await tourImageRepository.GetListByPredicateAsync(x => x.TourId == mainTour.Id);
                if (mainTourImagesDb != null)
                {
                    cache.Add($"Tour.TourDetails.GetTourImagesByTourIdEqual{mainTour.Id}", mainTourImagesDb, 1440);
                    mainTourImages = mainTourImagesDb;
                }
            }
            else
            {
                mainTourImages = mainTourImagesFromCache;
            }

            if (mainTourImages == null || !mainTourImages.Any())
            {
                TourDetails.TourImages.Add(PusulaGroupConstants.ImagePathNoImage);
            }
            else
            {
                foreach (var item in mainTourImages.Select((Value, Index) => new { Index, Value }))
                {
                    if (item.Value.IsMain)
                        ActiveTourImageIndex = item.Index;

                    TourDetails.TourImages.Add(item.Value.Path);
                }
            }
        }

        private async Task SetPropertiesForSubToursAsync(List<Tour> subTours)
        {
            TourDetails.IsMainTour = false;

            foreach (var subTour in subTours)
            {
                string imagePath;
                if (!cache.TryGet<string>($"Tour.TourDetails.GetSubTourMainImageBySubTourIdEqual{subTour.Id}", out var subTourMainImageFromCache))
                {
                    var subTourImageDb = await tourImageRepository.GetAsync(x => x.IsMain && x.TourId == subTour.Id);
                    imagePath = subTourImageDb == null
                                ? PusulaGroupConstants.ImagePathNoImage
                                : subTourImageDb.Path;
                    cache.Add($"Tour.TourDetails.GetSubTourMainImageBySubTourIdEqual{subTour.Id}", imagePath, 1440);
                }
                else
                {
                    imagePath = subTourMainImageFromCache;
                }

                TourDetails.SubTours.Add(new TourDetailDto
                {
                    Name = subTour.Name,
                    UserFriendlyName = subTour.UserFriendlyName,
                    ImagePath = imagePath
                });
            }
        }
    }
}
