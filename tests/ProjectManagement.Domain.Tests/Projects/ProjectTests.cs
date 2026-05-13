using ProjectManagement.Domain.Enums;
using ProjectManagement.Domain.Exceptions;
using ProjectManagement.Domain.Models;

namespace ProjectManagement.Domain.Tests.Projects;

public class ProjectTests
{
    [Fact]
    public void Constructor_Should_Set_Default_State()
    {
        var project = CreateProject();

        Assert.Equal(ProjectStatus.Active, project.Status);
        Assert.Equal(ProjectPriority.Medium, project.Priority);
        Assert.Empty(project.WorkTasks);
        Assert.Empty(project.Executors);
    }

    [Fact]
    public void Constructor_Should_Throw_When_Title_Is_Empty()
    {
        Assert.Throws<ArgumentNullException>(() =>
            new Project("", "desc", ProjectPriority.Medium)
        );
    }

    [Fact]
    public void Constructor_Should_Throw_When_Title_Whitespace()
    {
        Assert.Throws<ArgumentNullException>(() =>
            new Project("   ", "desc", ProjectPriority.Medium)
        );
    }

    [Fact]
    public void Constructor_Should_Throw_When_Title_Too_Long()
    {
        var title = new string('a', Project.MaxTitleLength + 1);

        Assert.Throws<ArgumentOutOfRangeException>(() =>
            new Project(title, "desc", ProjectPriority.Medium)
        );
    }

    [Fact]
    public void Complete_Should_Throw_When_Has_Incomplete_Tasks()
    {
        var project = CreateProject();

        project.CreateWorkTask("t", null, DateTime.UtcNow.AddDays(1));

        Assert.Throws<DomainRuleException>(() => project.Complete());
    }

    [Fact]
    public void Complete_Should_Work_When_All_Tasks_Completed()
    {
        var project = CreateProject();

        project.AddExecutor(Guid.NewGuid());
        var user = project.Executors.First().UserId;

        project.CreateWorkTask("t", null, DateTime.UtcNow.AddDays(1));
        var task = project.WorkTasks.First();

        project.AssignWorkTaskExecutor(task.Id, user);
        project.CompleteWorkTask(task.Id);

        project.Complete();

        Assert.Equal(ProjectStatus.Completed, project.Status);
    }

    [Fact]
    public void ChangeTitle_Should_Throw_When_Completed()
    {
        var project = CreateCompletedProject();

        Assert.Throws<DomainRuleException>(() => project.ChangeTitle("new"));
    }

    [Fact]
    public void ChangeDescription_Should_Work_When_Active()
    {
        var project = CreateProject();

        project.ChangeDescription("new");

        Assert.Equal("new", project.Description);
    }

    [Fact]
    public void Priority_Should_Be_Immutable()
    {
        var project = CreateProject();

        Assert.Equal(ProjectPriority.Medium, project.Priority);
    }

    private static Project CreateProject()
    {
        return new Project(
            "Project",
            "desc",
            ProjectPriority.Medium
        );
    }

    private static Project CreateCompletedProject()
    {
        var p = CreateProject();
        p.Complete();
        return p;
    }
}