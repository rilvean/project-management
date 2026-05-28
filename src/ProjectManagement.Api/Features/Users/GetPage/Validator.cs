using FluentValidation;

namespace ProjectManagement.Api.Features.Users.GetPage;

public class Validator : AbstractValidator<GetUsersPageQuery>
{
    public Validator()
    {
        RuleFor(x => x.Page)
            .GreaterThan(0);

        RuleFor(x => x.PageSize)
            .InclusiveBetween(1, 100);
    }
}