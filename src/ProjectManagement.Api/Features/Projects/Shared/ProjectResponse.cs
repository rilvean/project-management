using ProjectManagement.Domain.Enums;

namespace ProjectManagement.Api.Features.Projects.Shared;

public sealed record ProjectResponse(
    Guid Id,
    Guid? ManagerId,
    string Title,
    string? Description,
    ProjectPriority Priority,
    ProjectStatus Status
);