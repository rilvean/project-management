using MediatR;

namespace ProjectManagement.Api.Features.Projects.Complete;

public sealed record CompleteProjectCommand(
    Guid ProjectId,
    Guid ActorId
) : IRequest;