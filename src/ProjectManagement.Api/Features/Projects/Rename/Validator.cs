using FluentValidation;
using ProjectManagement.Domain.Models;

namespace ProjectManagement.Api.Features.Projects.Rename;

public class Validator : AbstractValidator<RenameProjectCommand>
{
    public Validator()
    {
        RuleFor(x => x.Title)
            .MaximumLength(Project.MaxDescriptionLength)
            .NotEmpty();

        RuleFor(x => x.ProjectId)
            .NotEmpty();
        
        RuleFor(x => x.ActorId)
            .NotEmpty();
    }
}