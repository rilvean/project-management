using MediatR;

namespace ProjectManagement.Api.Features.WorkTasks.Complete;

public record CompleteWorkTaskCommand(
    Guid WorkTaskId,
    Guid ActorId
) : IRequest;