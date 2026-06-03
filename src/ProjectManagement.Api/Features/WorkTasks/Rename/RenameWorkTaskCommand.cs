using MediatR;

namespace ProjectManagement.Api.Features.WorkTasks.Rename;

public record RenameWorkTaskCommand(
    Guid WorkTaskId,
    string Title,
    Guid ActorId
) : IRequest;