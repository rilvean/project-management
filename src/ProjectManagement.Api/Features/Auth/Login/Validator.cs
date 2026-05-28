using FluentValidation;
using ProjectManagement.Domain.ValueObjects;

namespace ProjectManagement.Api.Features.Auth.Login;

public sealed class Validator : AbstractValidator<LoginCommand>
{
    public Validator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .Matches(Email.Pattern);

        RuleFor(x => x.Password)
            .NotEmpty()
            .MinimumLength(8);
    }
}