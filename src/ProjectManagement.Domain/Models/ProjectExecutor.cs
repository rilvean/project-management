namespace ProjectManagement.Domain.Models;

public class ProjectExecutor
{
    public Guid ProjectId { get; private set; }
    public Guid UserId { get; private set; }


    internal ProjectExecutor(Guid projectId, Guid userId)
    {
        ProjectId = projectId;
        UserId = userId;
    }

    private ProjectExecutor() { }
}