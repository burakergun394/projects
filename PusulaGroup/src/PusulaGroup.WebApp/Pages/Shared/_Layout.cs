using PusulaGroup.WebApp.Application.Interfaces.Repositories;
using PusulaGroup.WebApp.Core.CrossCuttingConcerns.Caching;
using PusulaGroup.WebApp.Domain.Entities;

namespace PusulaGroup.WebApp.Pages.Shared
{
    public partial class _Layout
    {
        private readonly IGeneralInfoRepository generalInfoRepository;
        private readonly ICache cache;

        public GeneralInfo GeneralInfo { get; private set; }

        public _Layout(IGeneralInfoRepository generalInfoRepository, ICache cache)
        {
            this.generalInfoRepository = generalInfoRepository;
            this.cache = cache;

            SetGeneralInfo().Wait();
        }

        private async Task SetGeneralInfo()
        {
            if (!cache.TryGet<GeneralInfo>("GeneralInfo.Layout.Get", out var value))
            {
                GeneralInfo = await generalInfoRepository.AnyAsync(x => x.Id > 0)
                ? (await generalInfoRepository.GetListAsync()).First()
                : new GeneralInfo();
                cache.Add("GeneralInfo.Layout.Get", GeneralInfo, 1440);
            }
            else
            {
                GeneralInfo = value;
            }
        }
    }
}
