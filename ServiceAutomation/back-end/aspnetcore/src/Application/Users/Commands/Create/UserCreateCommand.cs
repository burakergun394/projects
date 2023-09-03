using Application.Helpers;
using Ardalis.GuardClauses;
using Domain.Responses;
using Domain.Shared.Enums;
using Domain.Users;
using FluentValidation;
using MediatR;

namespace Application.Users.Commands.Create;

public record UserCreateCommand(string Username, Guid RoleId, string Name, string Surname, string Email, string Password, Language Language) : IRequest<Response<User>>;

public class UserCreateCommandHandler : IRequestHandler<UserCreateCommand, Response<User>>
{
    private readonly IUserRepository _userRepository;

    public UserCreateCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Response<User>> Handle(UserCreateCommand request, CancellationToken cancellationToken)
    {
        Guard.Against.Null(request);
        PasswordHasherHelper.HashAndSaltPassword(request.Password, out var hash, out var salt);
        var addingUser = User.Create(request.Username, request.RoleId, request.Name, request.Surname, request.Email, hash, salt, request.Language);
        var addedUser = await _userRepository.CreateAsync(addingUser, cancellationToken);
        await _userRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        return Response<User>.Success(addedUser);
    }
}