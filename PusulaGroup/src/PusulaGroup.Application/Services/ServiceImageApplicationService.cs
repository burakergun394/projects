namespace PusulaGroup.Application.Services
{
    //public class ServiceImageApplicationService : BaseApplicationService<ServiceImage, IServiceImageRepository>, IServiceImageApplicationService
    //{
    //    private readonly ImageHelper imageHelper;
    //    public ServiceImageApplicationService(IServiceImageRepository repository, IUnitOfWork unitOfWork, ImageHelper imageHelper) : base(repository, unitOfWork)
    //    {
    //        this.imageHelper = imageHelper;
    //    }

    //    public async Task<ServiceImage> GetByPredicateAndSetNoImagePathIfImageNotFoundAsync(Expression<Func<ServiceImage, bool>> predicate)
    //    {
    //        var data = await GetByPredicateAsync(predicate);

    //        if (data == null)
    //            return null;

    //        data.Path = imageHelper.GetFullPath(data.Path);

    //        return data;
    //    }

    //    public async Task<List<ServiceImage>> GetListByPredicateAndSetNoImagePathIfImageNotFoundAsync(Expression<Func<ServiceImage, bool>> predicate)
    //    {
    //        var datas = await GetListByPredicateAsync(predicate);

    //        foreach (var data in datas)
    //        {
    //            data.Path = imageHelper.GetFullPath(data.Path);
    //        }

    //        return datas;
    //    }
    //}
}
