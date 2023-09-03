using Application.Exceptions;
using Application.Helpers;
using Ardalis.GuardClauses;
using Domain.Contexts;
using Domain.Responses;
using Domain.Users;
using FluentValidation;
using MediatR;

namespace Application.Features.Users.Commands.UpdatePassword;

public record UpdateUserPasswordCommand(string Username, string OldPassword, string NewPassword) : IRequest<Response<NoContentResponse>>;

public class UpdateUserPasswordCommandHandler : IRequestHandler<UpdateUserPasswordCommand, Response<NoContentResponse>>
{
    private readonly IUserRepository _userRepository;

    public UpdateUserPasswordCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Response<NoContentResponse>> Handle(UpdateUserPasswordCommand request, CancellationToken cancellationToken)
    {
        Guard.Against.Null(request);

        var user = await _userRepository.GetByUsernameAsync(request.Username, cancellationToken)
                    ?? throw new BusinessException("User or password wrong.");

        if (!PasswordHasherHelper.VerifyPassword(request.OldPassword, user.PasswordHash, user.PasswordSalt))
            throw new BusinessException("User or password wrong.");

        PasswordHasherHelper.HashAndSaltPassword(request.NewPassword, out var hash, out var salt);

        user.UpdatePassword(hash, salt);

        await _userRepository.UpdateAsync(user);
        await _userRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

        return Response<NoContentResponse>.Success("Password changed.");
    }
}

public class UpdateUserPasswordCommandValidator : AbstractValidator<UpdateUserPasswordCommand>
{
    public UpdateUserPasswordCommandValidator()
    {
        RuleFor(x => x.Username)
           .NotEmpty()
           .WithMessage("Username is required");

        RuleFor(x => x.OldPassword)
            .NotEmpty()
            .WithMessage("Old password is required");

        RuleFor(x => x.NewPassword)
            .NotEmpty()
            .WithMessage("New password is required");

        RuleFor(x => x.NewPassword)
            .Must(x => x.Length >= 8)
            .WithMessage("Password must be at least 8 characters long.");
    }
}