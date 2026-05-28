using FluentValidation;
using ProjectManagement.Domain.Models;

namespace ProjectManagement.Api.Features.Users.Rename;

public class Validator : AbstractValidator<RenameUserCommand>
{
    public Validator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty();

        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(User.NameMaxLength);
    }
}