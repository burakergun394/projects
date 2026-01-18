using FluentValidation;

namespace FollowCatcher.Application.Twitter.Commands.ShareProfileCard;

public class ShareProfileCardValidator : AbstractValidator<ShareProfileCardCommand>
{
    public ShareProfileCardValidator()
    {
        RuleFor(v => v.Username)
            .NotEmpty().WithMessage("Username is required.")
            .MaximumLength(30).WithMessage("Username must not exceed 30 characters.");

        RuleFor(v => v.TweetText)
            .NotEmpty().WithMessage("Tweet text is required.")
            .MaximumLength(280).WithMessage("Tweet text must not exceed 280 characters.");
    }
}
