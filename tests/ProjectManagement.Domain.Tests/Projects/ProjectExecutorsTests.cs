using ProjectManagement.Domain.Enums;
using ProjectManagement.Domain.Exceptions;
using ProjectManagement.Domain.Models;

namespace ProjectManagement.Domain.Tests.Projects;

public class ProjectExecutorsTests
{
    [Fact]
    public void AddExecutor_Should_Add_User()
    {
        var project = CreateProject();

        project.AddExecutor(Guid.NewGuid());

        Assert.Single(project.Executors);
    }

    [Fact]
    public void AddExecutor_Should_Be_Idempotent()
    {
        var project = CreateProject();

        var id = Guid.NewGuid();

        project.AddExecutor(id);
        project.AddExecutor(id);

        Assert.Single(project.Executors);
    }

    [Fact]
    public void AddExecutor_Should_Throw_When_Is_Manager()
    {
        var project = CreateProject();

        var id = Guid.NewGuid();

        project.AssignManager(id);

        Assert.Throws<DomainRuleException>(() => project.AddExecutor(id));
    }

    [Fact]
    public void RemoveExecutor_Should_Clear()
    {
        var project = CreateProject();

        var id = Guid.NewGuid();

        project.AddExecutor(id);
        project.RemoveExecutor(id);

        Assert.Empty(project.Executors);
    }

    [Fact]
    public void RemoveExecutor_Should_Throw_When_Has_Active_Tasks()
    {
        var project = CreateProject();

        var id = Guid.NewGuid();

        project.AddExecutor(id);

        project.CreateWorkTask("Task", null, DateTime.UtcNow.AddDays(1));
        var task = project.WorkTasks.First();

        project.AssignWorkTaskExecutor(task.Id, id);

        Assert.Throws<DomainRuleException>(() =>
            project.RemoveExecutor(id)
        );
    }

    [Fact]
    public void AddExecutor_Should_Throw_When_Project_Completed()
    {
        var project = CreateCompletedProject();

        Assert.Throws<DomainRuleException>(() =>
            project.AddExecutor(Guid.NewGuid())
        );
    }

    private static Project CreateProject() => new("Project", "desc", ProjectPriority.Medium);

    private static Project CreateCompletedProject()
    {
        var p = CreateProject();
        p.Complete();
        return p;
    }
}