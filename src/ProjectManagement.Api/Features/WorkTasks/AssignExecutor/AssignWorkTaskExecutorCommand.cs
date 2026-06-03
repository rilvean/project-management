using MediatR;

namespace ProjectManagement.Api.Features.WorkTasks.AssignExecutor;

public record AssignWorkTaskExecutorCommand(
    Guid WorkTaskId,
    Guid ExecutorId,
    Guid ActorId
) : IRequest;