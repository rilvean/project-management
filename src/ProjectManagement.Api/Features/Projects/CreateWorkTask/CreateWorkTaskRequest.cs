namespace ProjectManagement.Api.Features.Projects.CreateWorkTask;

public record CreateWorkTaskRequest(
    string Title,
    string? Description,
    DateTime? Deadline
);