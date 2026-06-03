using MediatR;

namespace ProjectManagement.Api.Features.Projects.RemoveExecutor;

public sealed record RemoveProjectExecutorCommand(
    Guid ProjectId,
    Guid ExecutorId,
    Guid ActorId
) : IRequest;