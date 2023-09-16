using Application.Abstractions.Localizations;
using Application.Behaviors.Authorization;
using Domain.Identity.Users;
using Domain.Shared.Responses;
using FluentValidation;
using MediatR;

namespace Application.Features.Identity.Users.Queries.GetById;

public record UserGetByIdQuery(Guid Id) : IRequest<Response<User>>, IAuthorizationClaimRequest
{
    public string Claim { get; } = "user.get";
}

public class UserGetByIdQueryHandler : IRequestHandler<UserGetByIdQuery, Response<User>>
{
    private readonly IUserRepository _userRepository;
    private readonly ILocalizationService _localizationService;

    public UserGetByIdQueryHandler(IUserRepository userRepository, ILocalizationService localizationService)
    {
        _userRepository = userRepository;
        _localizationService = localizationService;
    }

    public async Task<Response<User>> Handle(UserGetByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.Id, cancellationToken);

        return user is null 
            ? Response<User>.Failure(_localizationService["UserNotFound"]) 
            : Response<User>.Success(user);
    }
}

public class UserGetByIdQueryValidator : AbstractValidator<UserGetByIdQuery>
{
    public UserGetByIdQueryValidator(ILocalizationService localizationService)
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .NotNull()
            .WithMessage(localizationService["Required:{0}", nameof(UserGetByIdQuery.Id)]);
    }
}