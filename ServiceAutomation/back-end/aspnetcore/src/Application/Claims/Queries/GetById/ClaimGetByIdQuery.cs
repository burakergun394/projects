using Domain.Claims;
using Domain.Responses;
using MediatR;

namespace Application.Roles.Queries.GetById;

public record ClaimGetByIdQuery(Guid Id) : IRequest<Response<Claim>>;

public class ClaimGetByIdQueryHandler : IRequestHandler<ClaimGetByIdQuery, Response<Claim>>
{
    private readonly IClaimRepository _claimRepository;
    public ClaimGetByIdQueryHandler(IClaimRepository claimRepository)
    {
        _claimRepository = claimRepository;
    }

    public async Task<Response<Claim>> Handle(ClaimGetByIdQuery request, CancellationToken cancellationToken)
    {
        var claim = await _claimRepository.GetByIdAsync(request.Id, cancellationToken);
        return claim is null ? Response<Claim>.Failure("Role not found.") : Response<Claim>.Success(claim);
    }
}