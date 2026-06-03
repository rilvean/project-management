using MediatR;

namespace ProjectManagement.Api.Features.WorkTasks.Delete;

public sealed record DeleteWorkTaskCommand(
    Guid WorkTaskId,
    Guid ActorId
) : IRequest;