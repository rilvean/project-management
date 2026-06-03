using ProjectManagement.Domain.Models;

namespace ProjectManagement.Domain.Services;

public static class ProjectPolicies
{
    public static bool CanEdit(Project project, Guid userId)
    {
        if (project.ManagerId == userId) return true;

        return false;
    }
}