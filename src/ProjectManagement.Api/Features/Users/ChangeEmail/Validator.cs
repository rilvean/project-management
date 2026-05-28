using FluentValidation;
using ProjectManagement.Domain.ValueObjects;

namespace ProjectManagement.Api.Features.Users.ChangeEmail;

public class Validator : AbstractValidator<ChangeEmailCommand>
{
    public Validator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty();

        RuleFor(x => x.Email)
            .NotEmpty()
            .MaximumLength(Email.MaxLenght)
            .Matches(Email.Pattern);
    }
}