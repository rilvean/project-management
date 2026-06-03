using MediatR;

namespace ProjectManagement.Api.Features.WorkTasks.Delete;

public record DeleteWorkTaskCommand(
    Guid WorkTaskId,
    Guid ActorId
) : IRequest;