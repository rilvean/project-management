using MediatR;

namespace ProjectManagement.Api.Features.WorkTasks.AssignExecutor;

public sealed record AssignWorkTaskExecutorCommand(
    Guid WorkTaskId,
    Guid ExecutorId,
    Guid ActorId
) : IRequest;