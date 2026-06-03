using FluentValidation;
using ProjectManagement.Api.Features.WorkTasks.ChangeDescription;
using ProjectManagement.Domain.Models;

namespace ProjectManagement.Api.Features.WorkTasks.Rename;

public class Validator : AbstractValidator<RenameWorkTaskCommand>
{
    public Validator()
    {
        RuleFor(x => x.Title)
            .MaximumLength(WorkTask.MaxTitleLength);

        RuleFor(x => x.WorkTaskId)
            .NotEmpty();

        RuleFor(x => x.ActorId)
            .NotEmpty();
    }
}