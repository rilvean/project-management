using ProjectManagement.Domain.Enums;

namespace ProjectManagement.Api.Features.Shared;

public record WorkTaskResponse(
    Guid Id,
    Guid ProjectId,
    Guid? ExecutorId,
    string Title,
    string? Description,
    DateTime? Deadline,
    WorkTaskStatus Status
);