using ProjectManagement.Domain.Enums;
using ProjectManagement.Domain.Exceptions;
using ProjectManagement.Domain.Models;

namespace ProjectManagement.Domain.Tests.Projects;

public class ProjectWorkTasksTests
{
    [Fact]
    public void CreateWorkTask_Should_Add_Task()
    {
        var project = CreateProject();

        project.CreateWorkTask("Task", null, DateTime.UtcNow.AddDays(1));

        Assert.Single(project.WorkTasks);
    }

    [Fact]
    public void CreateWorkTask_Should_Throw_When_Title_Empty()
    {
        var project = CreateProject();

        Assert.Throws<ArgumentNullException>(() =>
            project.CreateWorkTask("", null, DateTime.UtcNow.AddDays(1))
        );
    }

    [Fact]
    public void CreateWorkTask_Should_Throw_When_Project_Completed()
    {
        var project = CreateCompletedProject();

        Assert.Throws<DomainRuleException>(() =>
            project.CreateWorkTask("Task", null, DateTime.UtcNow.AddDays(1))
        );
    }

    [Fact]
    public void AssignExecutor_Should_Throw_When_User_Not_In_Project()
    {
        var project = CreateProject();

        project.CreateWorkTask("Task", null, DateTime.UtcNow.AddDays(1));

        Assert.Throws<DomainRuleException>(() =>
            project.AssignWorkTaskExecutor(project.WorkTasks.First().Id, Guid.CreateVersion7())
        );
    }

    [Fact]
    public void AssignExecutor_Should_Work_When_User_In_Project()
    {
        var project = CreateProject();

        var user = Guid.CreateVersion7();
        project.AddExecutor(user);

        project.CreateWorkTask("Task", null, DateTime.UtcNow.AddDays(1));

        var task = project.WorkTasks.First();

        project.AssignWorkTaskExecutor(task.Id, user);

        Assert.Equal(user, task.ExecutorId);
    }

    [Fact]
    public void RemoveWorkTask_Should_Delete_Task()
    {
        var project = CreateProject();

        project.CreateWorkTask("Task", null, DateTime.UtcNow.AddDays(1));

        var task = project.WorkTasks.First();

        project.RemoveWorkTask(task.Id);

        Assert.Empty(project.WorkTasks);
    }

    private static Project CreateProject() => new("Project", "desc", ProjectPriority.Medium);

    private static Project CreateCompletedProject()
    {
        var p = CreateProject();
        p.Complete();
        return p;
    }
}