using Application.Abstractions.Localizations;
using Domain.Identity.Tenants;
using Domain.Shared.Responses;
using MediatR;

namespace Application.Features.Identity.Tenants.Queries.GetActiveList;

public record TenantGetActiveListQuery() : IRequest<Response<List<string>>>;

public class TenantGetActiveListQueryHandler : IRequestHandler<TenantGetActiveListQuery, Response<List<string>>>
{
    private readonly ITenantRepository _tenantRepository;
    private readonly ILocalizationService _localizationService;

    public TenantGetActiveListQueryHandler(ITenantRepository tenantRepository, ILocalizationService localizationService)
    {
        _tenantRepository = tenantRepository;
        _localizationService = localizationService;
    }

    public async Task<Response<List<string>>> Handle(TenantGetActiveListQuery request, CancellationToken cancellationToken)
    {
        var tenants = await _tenantRepository.GetActiveListAsync(cancellationToken);

        return tenants is null || tenants.Count == 0
            ? Response<List<string>>.Failure(_localizationService["TenantsNotFound"]) 
            : Response<List<string>>.Success(tenants);
    }
}