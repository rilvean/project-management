using MediatR;

namespace ProjectManagement.Api.Features.WorkTasks.ChangeDeadline;

public sealed record ChangeWorkTaskDeadlineCommand(
    Guid WorkTaskId,
    DateTime? Deadline,
    Guid ActorId
) : IRequest;