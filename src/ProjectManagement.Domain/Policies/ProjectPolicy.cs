using ProjectManagement.Domain.Enums;
using ProjectManagement.Domain.Models;

namespace ProjectManagement.Domain.Policies;

public static class ProjectPolicy
{
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