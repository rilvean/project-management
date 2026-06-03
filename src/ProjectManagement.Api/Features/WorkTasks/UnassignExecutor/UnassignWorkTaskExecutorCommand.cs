using MediatR;

namespace ProjectManagement.Api.Features.WorkTasks.UnassignExecutor;

public sealed record UnassignWorkTaskExecutorCommand(
    Guid WorkTaskId,
    Guid ActorId
) : IRequest;