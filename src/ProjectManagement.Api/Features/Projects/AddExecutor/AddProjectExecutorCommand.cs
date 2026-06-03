using MediatR;

namespace ProjectManagement.Api.Features.Projects.AddExecutor;

public sealed record AddProjectExecutorCommand(
    Guid ProjectId,
    Guid ExecutorId,
    Guid ActorId
) : IRequest;