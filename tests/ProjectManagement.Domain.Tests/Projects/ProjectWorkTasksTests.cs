using ProjectManagement.Domain.Exceptions;

namespace ProjectManagement.Domain.Tests.Projects;

public class ProjectWorkTasksTests
{
    [Fact]
    public void CreateWorkTask_Should_Add_Task()
    {
        var project = ProjectTestData.CreateProject();

        project.CreateWorkTask("Task", null, DateTime.UtcNow.AddDays(1));

        Assert.Single(project.WorkTasks);
    }

    [Fact]
    public void CreateWorkTask_Should_Throw_When_Project_Completed()
    {
        var project = ProjectTestData.CreateCompletedProject();

        Assert.Throws<DomainRuleException>(() =>
            project.CreateWorkTask("Task", null, DateTime.UtcNow.AddDays(1))
        );
    }

    [Fact]
    public void AssignExecutor_Should_Throw_When_User_Not_In_Project()
    {
        var project = ProjectTestData.CreateProject();

        project.CreateWorkTask("Task", null, DateTime.UtcNow.AddDays(1));

        Assert.Throws<DomainRuleException>(() =>
            project.AssignWorkTaskExecutor(project.WorkTasks.First().Id, Guid.CreateVersion7())
        );
    }

    [Fact]
    public void AssignExecutor_Should_Work_When_User_In_Project()
    {
        var project = ProjectTestData.CreateProject();

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
        var project = ProjectTestData.CreateProject();

        project.CreateWorkTask("Task", null, DateTime.UtcNow.AddDays(1));

        var task = project.WorkTasks.First();

        project.RemoveWorkTask(task.Id);

        Assert.Empty(project.WorkTasks);
    }
}