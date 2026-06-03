using MediatR;

namespace ProjectManagement.Api.Features.WorkTasks.Complete;

public sealed record CompleteWorkTaskCommand(
    Guid WorkTaskId,
    Guid ActorId
) : IRequest;