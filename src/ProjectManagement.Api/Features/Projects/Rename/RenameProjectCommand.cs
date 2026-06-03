using MediatR;

namespace ProjectManagement.Api.Features.Projects.Rename;

public sealed record RenameProjectCommand(
    Guid ProjectId,
    string Title,
    Guid ActorId
) : IRequest;