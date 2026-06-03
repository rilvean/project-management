using ProjectManagement.Domain.Models;

namespace ProjectManagement.Domain.Services;

public static class WorkTaskPolicies
{
    public static bool CanComplete(Project project, Guid workTaskId, Guid userId)
    {
        var workTask = project.WorkTasks.FirstOrDefault(w => w.Id == workTaskId);
        if (workTask is null) return false;

        if (project.ManagerId == userId) return true;
        if (workTask.ExecutorId == userId) return true;

        return false;
    }

    public static bool CanEdit(Project project, Guid userId)
    {
        if (project.ManagerId == userId) return true;

        return false;
    }
}