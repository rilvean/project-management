using MediatR;

namespace ProjectManagement.Api.Features.Projects.Delete;

public sealed record DeleteProjectCommand(
    Guid ProjectId,
    Guid ActorId
) : IRequest;