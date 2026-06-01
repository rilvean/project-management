using MediatR;

namespace ProjectManagement.Api.Features.Projects.CreateWorkTask;

public record CreateWorkTaskCommand(
    Guid ProjectId,
    string Title,
    string? Description,
    DateTime? Deadline
) : IRequest<CreateWorkTaskResponse>;