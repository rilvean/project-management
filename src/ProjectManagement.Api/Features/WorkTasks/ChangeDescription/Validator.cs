using FluentValidation;
using ProjectManagement.Domain.Models;

namespace ProjectManagement.Api.Features.WorkTasks.ChangeDescription;

public sealed class Validator : AbstractValidator<ChangeWorkTaskDescriptionCommand>
{
    public Validator()
    {
        RuleFor(x => x.Description)
            .MaximumLength(WorkTask.MaxDescriptionLength);

        RuleFor(x => x.WorkTaskId)
            .NotEmpty();

        RuleFor(x => x.ActorId)
            .NotEmpty();
    }
}