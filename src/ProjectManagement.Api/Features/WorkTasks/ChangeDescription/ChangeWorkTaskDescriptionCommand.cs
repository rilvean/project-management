using MediatR;

namespace ProjectManagement.Api.Features.WorkTasks.ChangeDescription;

public sealed record ChangeWorkTaskDescriptionCommand(
    Guid WorkTaskId,
    string? Description,
    Guid ActorId
) : IRequest;