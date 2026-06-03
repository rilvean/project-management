using MediatR;

namespace ProjectManagement.Api.Features.Projects.Complete;

public record CompleteProjectCommand(
    Guid ProjectId,
    Guid ActorId
) : IRequest;