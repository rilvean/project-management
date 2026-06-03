using MediatR;

namespace ProjectManagement.Api.Features.Projects.ChangeDescription;

public sealed record ChangeProjectDescriptionCommand(
    Guid ProjectId,
    string? Description,
    Guid ActorId
) : IRequest;