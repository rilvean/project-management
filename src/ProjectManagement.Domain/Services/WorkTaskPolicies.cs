using ProjectManagement.Domain.Enums;
using ProjectManagement.Domain.Models;

namespace ProjectManagement.Domain.Services;

public static class WorkTaskPolicies
{
    public static bool CanComplete(Project project, Guid workTaskId, User user)
    {
        var workTask = project.WorkTasks.FirstOrDefault(w => w.Id == workTaskId);
        if (workTask is null) return false;

        return user.Role switch
        {
            UserRole.Employee => workTask.ExecutorId == user.Id,
            UserRole.ProjectManager => project.ManagerId == user.Id,
            UserRole.Supervisor => true,
            _ => false
        };
    }

    public static bool CanEdit(Project project, User user)
    {
        return user.Role switch
        {
            UserRole.ProjectManager => project.ManagerId == user.Id,
            UserRole.Supervisor => true,
            _ => false
        };
    }
}