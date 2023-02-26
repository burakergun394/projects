using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PusulaGroup.WebApp.Application.Dtos;
using PusulaGroup.WebApp.Application.Interfaces.Repositories;
using PusulaGroup.WebApp.Core.CrossCuttingConcerns.Caching;
using PusulaGroup.WebApp.Domain.Constants;
using PusulaGroup.WebApp.Domain.Entities;

namespace PusulaGroup.WebApp.Pages
{
    public class IndexTourDetail
    {
        public string Name { get; set; }
        public string UserFriendlyName { get; set; }
        public string ImagePath { get; set; }
    }

    public class IndexSliderDetail
    {
        public string Name { get; set; }
        public string UserFriendlyName { get; set; }
        public string Text { get; set; }
        public string Url { get; set; }
    }

    public class IndexServiceDetail
    {
        public string Name { get; set; }
        public string UserFriendlyName { get; set; }
        public string ImagePath { get; set; }
    }

    public class IndexViewModel
    {
        public List<OurCustomer> OurCustomers { get; set; }
        public List<IndexTourDetail> Tours { get; set; }
        public List<IndexServiceDetail> Services { get; set; }
        public List<IndexSliderDetail> Sliders { get; set; }

        public int DivisionTour { get; set; }
        public int NumberOfToursInRow { get; set; }

        public int DivisionService { get; set; }
        public int NumberOfServicesInRow { get; set; }
    }

    public class IndexModel : PageModel
    {
        private readonly IOurCustomerRepository ourCustomerRepository;
        private readonly ITourRepository tourRepository;
        private readonly ITourImageRepository tourImageRepository;
        private readonly IServiceRepository serviceRepository;
        private readonly IServiceImageRepository serviceImageRepository;
        private readonly IGeneralInfoRepository generalInfoRepository;
        private readonly ICache cache;

        [BindProperty]
        public IndexViewModel IndexViewModel { get; set; }

        public IndexModel(IOurCustomerRepository ourCustomerRepository, ITourRepository tourRepository, ITourImageRepository tourImageRepository, IServiceRepository serviceRepository, IServiceImageRepository serviceImageRepository, ICache cache, IGeneralInfoRepository generalInfoRepository)
        {
            this.ourCustomerRepository = ourCustomerRepository;
            this.tourRepository = tourRepository;
            this.tourImageRepository = tourImageRepository;
            this.serviceRepository = serviceRepository;
            this.serviceImageRepository = serviceImageRepository;

            IndexViewModel = new IndexViewModel
            {
                OurCustomers = new List<OurCustomer>(),
                Tours = new List<IndexTourDetail>(),
                Services = new List<IndexServiceDetail>(),
                Sliders = new List<IndexSliderDetail>(),
                NumberOfToursInRow = 3,
                NumberOfServicesInRow = 3
            };
            this.cache = cache;
            this.generalInfoRepository = generalInfoRepository;
        }

        public async Task OnGet()
        {
            IndexViewModel.OurCustomers = await GetOurCustomersAsync();
            IndexViewModel.Tours = await GetToursAsync();
            IndexViewModel.Sliders = await GetSliderToursAsync();
            IndexViewModel.Services = await GetServicesAsync();
            IndexViewModel.DivisionTour = GetDivision(IndexViewModel.Tours.Count, IndexViewModel.NumberOfToursInRow);
            IndexViewModel.DivisionService = GetDivision(IndexViewModel.Services.Count, IndexViewModel.NumberOfServicesInRow);

            var generalInfo = await GetGeneralInfo();
            ViewData["title"] = "Ana Sayfa";
            ViewData["description"] = generalInfo.SeoDescription;
            ViewData["keywords"] = generalInfo.SeoKeywords;
            ViewData["author"] = generalInfo.SeoAuthor;
        }

        private async Task<List<OurCustomer>> GetOurCustomersAsync()
        {
            var result = new List<OurCustomer>();
            if (!cache.TryGet<List<OurCustomer>>("OurCustomer.Index.GetOurCustomers", out var value))
            {
                var datas = await ourCustomerRepository.GetListAsync();
                if (datas == null)
                    return result;

                result.AddRange(datas);
                cache.Add("OurCustomer.Index.GetOurCustomers", result, 1440);
            }
            else
            {
                result.AddRange(value);
            }


            return result;
        }

        private async Task<List<IndexTourDetail>> GetToursAsync()
        {
            var result = new List<IndexTourDetail>();

            if (!cache.TryGet<List<IndexTourDetail>>("Tour.Index.GetTours", out var value))
            {
                var datas = await tourRepository.GetListByPredicateAsync(x => x.ParentId == 0);

                if (datas == null)
                    return result;

                foreach (var data in datas)
                {
                    var mainImage = await tourImageRepository.GetAsync(x => x.IsMain && x.TourId == data.Id);
                    var mainImagePath = mainImage == null ? PusulaGroupConstants.ImagePathNoImage : mainImage.Path;
                    result.Add(new IndexTourDetail
                    {
                        Name = data.Name,
                        UserFriendlyName = data.UserFriendlyName,
                        ImagePath = mainImagePath
                    });
                }

                cache.Add("Tour.Index.GetTours", result, 1440);
            }
            else
            {
                result.AddRange(value);
            }

            return result;
        }

        private async Task<List<IndexSliderDetail>> GetSliderToursAsync()
        {
            var result = new List<IndexSliderDetail>();

            if (!cache.TryGet<List<IndexSliderDetail>>("Tour.Index.GetSliderTours", out var value))
            {
                var datas = await tourRepository.GetListByPredicateAsync(x => x.IsShowSlider);

                if (datas == null)
                    return result;

                foreach (var data in datas.OrderBy(x => x.SliderOrderIndex))
                {
                    result.Add(new IndexSliderDetail
                    {
                        UserFriendlyName = data.UserFriendlyName,
                        Text = data.SliderText,
                        Url = $"/turlarimiz/{data.Name}",
                        Name = data.Name
                    });
                }
                cache.Add("Tour.Index.GetSliderTours", result, 1440);
            }
            else
            {
                result.AddRange(value);
            }

            return result;
        }

        private async Task<List<IndexServiceDetail>> GetServicesAsync()
        {
            var result = new List<IndexServiceDetail>();

            if (!cache.TryGet<List<IndexServiceDetail>>("Service.Index.GetServices", out var value))
            {
                var datas = await serviceRepository.GetListAsync();

                if (datas == null)
                    return result;

                foreach (var data in datas)
                {
                    var mainImage = await serviceImageRepository.GetAsync(x => x.IsMain && x.ServiceId == data.Id);
                    var mainImagePath = mainImage == null ? PusulaGroupConstants.ImagePathNoImage : mainImage.Path;
                    result.Add(new IndexServiceDetail
                    {
                        Name = data.Name,
                        UserFriendlyName = data.UserFriendlyName,
                        ImagePath = mainImagePath
                    });
                }

                cache.Add("Service.Index.GetServices", result, 1440);
            }
            else
            {
                result.AddRange(value);
            }

            return result;
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

        private int GetDivision(int count, int numberOfValuesInRow)
        {
            return count / numberOfValuesInRow;
        }
    }
}