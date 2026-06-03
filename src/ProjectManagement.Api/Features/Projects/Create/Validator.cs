using FluentValidation;
using ProjectManagement.Domain.Models;

namespace ProjectManagement.Api.Features.Projects.Create;

public sealed class Validator : AbstractValidator<CreateProjectCommand>
{
    public Validator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .MaximumLength(Project.MaxTitleLength);

        RuleFor(x => x.Description)
            .MaximumLength(Project.MaxDescriptionLength);

        RuleFor(x => x.Priority)
            .IsInEnum();
    }
}