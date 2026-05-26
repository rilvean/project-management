using FluentValidation;
using ProjectManagement.Domain.Models;
using ProjectManagement.Domain.ValueObjects;

namespace ProjectManagement.Api.Features.Auth.Register;

public sealed class Validator : AbstractValidator<RegisterCommand>
{
    public Validator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(User.NameMaxLength);

        RuleFor(x => x.Email)
            .NotEmpty()
            .MaximumLength(Email.MaxLenght)
            .Matches(Email.Pattern);

        RuleFor(x => x.Password)
            .NotEmpty();

        RuleFor(x => x.Role)
            .IsInEnum();
    }
}