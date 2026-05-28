using FluentValidation;

namespace ProjectManagement.Api.Features.Users.ChangePassword;

public class Validator : AbstractValidator<ChangePasswordCommand>
{
    public Validator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty();

        RuleFor(x => x.Password)
            .NotEmpty()
            .MinimumLength(8);
    }
}