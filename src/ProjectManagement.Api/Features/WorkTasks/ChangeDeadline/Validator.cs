using FluentValidation;

namespace ProjectManagement.Api.Features.WorkTasks.ChangeDeadline;

public sealed class Validator : AbstractValidator<ChangeWorkTaskDeadlineCommand>
{
    public Validator()
    {
        RuleFor(x => x.Deadline)
            .GreaterThan(x => DateTime.UtcNow)
            .When(x => x.Deadline.HasValue);

        RuleFor(x => x.WorkTaskId)
            .NotEmpty();

        RuleFor(x => x.ActorId)
            .NotEmpty();
    }
}