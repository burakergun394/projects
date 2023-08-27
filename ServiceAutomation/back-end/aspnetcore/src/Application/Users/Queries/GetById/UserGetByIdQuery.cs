using Application.Exceptions;
using Domain.Responses;
using Domain.Users;
using MediatR;

namespace Application.Users.Queries.GetById;

public record UserGetByIdQuery(Guid Id) : IRequest<Response<User>>;

public class UserGetByIdQueryHandler : IRequestHandler<UserGetByIdQuery, Response<User>>
{
    private readonly IUserRepository _userRepository;
    public UserGetByIdQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Response<User>> Handle(UserGetByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.Id, cancellationToken);
        return user is null ? throw new BusinessException("User not found.") : Response<User>.Success(user);
    }
}