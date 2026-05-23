using ProjectManagement.Domain.Enums;
using ProjectManagement.Domain.Models;

namespace ProjectManagement.Domain.Tests.Projects;

static class ProjectTestData
{
    public static Project CreateProject(
        string title = "Project",
        string? description = "Description",
        ProjectPriority priority = ProjectPriority.Medium)
    {
        return new Project(title, description, priority);
    }

    public static Project CreateCompletedProject()
    {
        var project = CreateProject();

        project.Complete();

        return project;
    }
}