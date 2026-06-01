using FluentValidation;
using ProjectManagement.Domain.Models;

namespace ProjectManagement.Api.Features.Projects.CreateWorkTask;

public class Validator : AbstractValidator<CreateWorkTaskCommand>
{
    public Validator()
    {
        RuleFor(x => x.ProjectId)
            .NotEmpty();

        RuleFor(x => x.Title)
            .NotEmpty()
            .MaximumLength(WorkTask.MaxTitleLength);

        RuleFor(x => x.Description)
            .MaximumLength(WorkTask.MaxDescriptionLength);

        RuleFor(x => x.Deadline)
            .Must(x => x is null || x > DateTime.UtcNow)
            .WithMessage("Deadline must be in the future.");
    }
}