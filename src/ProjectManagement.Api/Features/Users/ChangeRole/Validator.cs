using FluentValidation;

namespace ProjectManagement.Api.Features.Users.ChangeRole;

public class Validator : AbstractValidator<ChangeRoleCommand>
{
    public Validator()
    {
        RuleFor(user => user.UserId)
            .NotEmpty();

        RuleFor(user => user.Role)
            .IsInEnum();
    }
}