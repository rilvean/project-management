using ProjectManagement.Domain.Enums;
using ProjectManagement.Domain.Models;

namespace ProjectManagement.Domain.Tests.Projects;

static class ProjectTestData
{
    public static Project CreateProject() => new("Project", "desc", ProjectPriority.Medium);

    public static Project CreateCompletedProject()
    {
        var p = CreateProject();
        p.Complete();
        return p;
    }
}