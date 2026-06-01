using FluentValidation;

namespace ProjectManagement.Api.Features.Projects.GetPage;

public class Validator :  AbstractValidator<GetProjectsPageQuery>
{
    public Validator()
    {
        RuleFor(x => x.Page)
            .GreaterThan(0);

        RuleFor(x => x.PageSize)
            .InclusiveBetween(1, 100);
    }
}