namespace ProjectManagement.Api.Features.Projects.CreateWorkTask;

public sealed record CreateWorkTaskRequest(
    string Title,
    string? Description,
    DateTime? Deadline
);