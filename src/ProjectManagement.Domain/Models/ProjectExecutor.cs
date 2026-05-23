namespace ProjectManagement.Domain.Models;

public class ProjectExecutor
{
    public Guid ProjectId { get; private init; }
    public Guid UserId { get; private init; }


    internal ProjectExecutor(Guid projectId, Guid userId)
    {
        ProjectId = projectId;
        UserId = userId;
    }

    private ProjectExecutor() { }
}