using MediatR;

namespace ProjectManagement.Api.Features.WorkTasks.UnassignExecutor;

public record UnassignWorkTaskExecutorCommand(
    Guid WorkTaskId,
    Guid ActorId
) : IRequest;