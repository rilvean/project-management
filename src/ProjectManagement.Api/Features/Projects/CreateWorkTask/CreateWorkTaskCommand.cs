using MediatR;

namespace ProjectManagement.Api.Features.Projects.CreateWorkTask;

public sealed record CreateWorkTaskCommand(
    Guid ProjectId,
    string Title,
    string? Description,
    DateTime? Deadline,
    Guid ActorId
) : IRequest<CreateWorkTaskResponse>;