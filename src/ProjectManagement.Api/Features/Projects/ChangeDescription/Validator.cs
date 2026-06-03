using FluentValidation;
using ProjectManagement.Domain.Models;

namespace ProjectManagement.Api.Features.Projects.ChangeDescription;

public class Validator : AbstractValidator<ChangeProjectDescriptionCommand>
{
    public Validator()
    {
        RuleFor(x => x.Description)
            .MaximumLength(Project.MaxDescriptionLength);

        RuleFor(x => x.ProjectId)
            .NotEmpty();

        RuleFor(x => x.ActorId)
            .NotEmpty();
    }
}