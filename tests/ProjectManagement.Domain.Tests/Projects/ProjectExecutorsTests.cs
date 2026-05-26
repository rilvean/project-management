using ProjectManagement.Domain.Exceptions;

namespace ProjectManagement.Domain.Tests.Projects;

public class ProjectExecutorsTests
{
    [Fact]
    public void AddExecutor_Should_Add_User()
    {
        var project = ProjectTestData.CreateProject();

        project.AddExecutor(Guid.CreateVersion7());

        Assert.Single(project.Executors);
    }

    [Fact]
    public void AddExecutor_Should_Be_Idempotent()
    {
        var project = ProjectTestData.CreateProject();

        var id = Guid.CreateVersion7();

        project.AddExecutor(id);
        project.AddExecutor(id);

        Assert.Single(project.Executors);
    }

    [Fact]
    public void AddExecutor_Should_Throw_When_Is_Manager()
    {
        var project = ProjectTestData.CreateProject();

        var id = Guid.CreateVersion7();

        project.AssignManager(id);

        Assert.Throws<DomainRuleException>(() => project.AddExecutor(id));
    }

    [Fact]
    public void RemoveExecutor_Should_Clear()
    {
        var project = ProjectTestData.CreateProject();

        var id = Guid.CreateVersion7();

        project.AddExecutor(id);
        project.RemoveExecutor(id);

        Assert.Empty(project.Executors);
    }

    [Fact]
    public void RemoveExecutor_Should_Throw_When_Has_Active_Tasks()
    {
        var project = ProjectTestData.CreateProject();

        var id = Guid.CreateVersion7();

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
        var project = ProjectTestData.CreateCompletedProject();

        Assert.Throws<DomainRuleException>(() =>
            project.AddExecutor(Guid.CreateVersion7())
        );
    }

    [Fact]
    public void RemoveExecutor_Should_Unassign_From_Completed_Tasks()
    {
        var project = ProjectTestData.CreateProject();

        var id = Guid.CreateVersion7();
        project.AddExecutor(id);

        project.CreateWorkTask("Task", null, DateTime.UtcNow.AddDays(1));
        var task = project.WorkTasks.First();

        project.AssignWorkTaskExecutor(task.Id, id);
        project.CompleteWorkTask(task.Id);

        project.RemoveExecutor(id);

        Assert.Null(task.ExecutorId);
        Assert.Empty(project.Executors);
    }
}