using ProjectManagement.Domain.Enums;
using ProjectManagement.Domain.Exceptions;
using ProjectManagement.Domain.Models;

namespace ProjectManagement.Domain.Tests.WorkTasks;

public class WorkTaskTests
{
    [Fact]
    public void Create_Should_Set_Default_State()
    {
        var task = CreateTask();

        Assert.Equal(WorkTaskStatus.Active, task.Status);
        Assert.Null(task.ExecutorId);
    }

    [Fact]
    public void Create_Should_Throw_When_Title_Is_Empty()
    {
        Assert.Throws<ArgumentNullException>(() =>
            WorkTask.Create(
                Guid.NewGuid(),
                "",
                "desc",
                DateTime.UtcNow.AddDays(1))
        );
    }

    [Fact]
    public void Create_Should_Throw_When_Title_Is_Whitespace()
    {
        Assert.Throws<ArgumentNullException>(() =>
            WorkTask.Create(
                Guid.NewGuid(),
                "   ",
                "desc",
                DateTime.UtcNow.AddDays(1))
        );
    }

    [Fact]
    public void Create_Should_Throw_When_Title_Too_Long()
    {
        var title = new string('a', WorkTask.MaxTitleLength + 1);

        Assert.Throws<ArgumentOutOfRangeException>(() =>
            WorkTask.Create(
                Guid.NewGuid(),
                title,
                "desc",
                DateTime.UtcNow.AddDays(1))
        );
    }

    [Fact]
    public void SetDeadline_Should_Accept_Null()
    {
        var task = CreateTask();

        task.SetDeadline(null);

        Assert.Null(task.Deadline);
    }

    [Fact]
    public void SetDeadline_Should_Throw_When_In_Past()
    {
        var task = CreateTask();

        Assert.Throws<ArgumentOutOfRangeException>(() =>
            task.SetDeadline(DateTime.UtcNow.AddDays(-1))
        );
    }

    [Fact]
    public void AssignExecutor_Should_Be_Idempotent()
    {
        var task = CreateTask();

        var id = Guid.NewGuid();

        task.AssignExecutor(id);
        task.AssignExecutor(id);

        Assert.Equal(id, task.ExecutorId);
    }

    [Fact]
    public void Complete_Should_Throw_When_No_Executor()
    {
        var task = CreateTask();

        Assert.Throws<DomainRuleException>(() => task.Complete());
    }

    [Fact]
    public void Complete_Should_Be_Idempotent()
    {
        var task = CreateTask();

        var id = Guid.NewGuid();

        task.AssignExecutor(id);

        task.Complete();
        task.Complete();

        Assert.Equal(WorkTaskStatus.Completed, task.Status);
    }

    [Fact]
    public void Rename_Should_Throw_When_Empty()
    {
        var task = CreateTask();

        Assert.Throws<ArgumentNullException>(() => task.Rename(""));
    }

    [Fact]
    public void Rename_Should_Throw_When_Whitespace()
    {
        var task = CreateTask();

        Assert.Throws<ArgumentNullException>(() => task.Rename(" "));
    }

    [Fact]
    public void Rename_Should_Throw_When_Completed()
    {
        var task = CreateCompletedTask();

        Assert.Throws<DomainRuleException>(() => task.Rename("new"));
    }

    [Fact]
    public void UnassignExecutor_Should_Work_Even_When_Completed()
    {
        var task = CreateCompletedTask();

        task.UnassignExecutor();

        Assert.Null(task.ExecutorId);
    }

    private static WorkTask CreateTask()
    {
        return WorkTask.Create(
            Guid.NewGuid(),
            "task",
            "desc",
            DateTime.UtcNow.AddDays(1)
        );
    }

    private static WorkTask CreateCompletedTask()
    {
        var t = CreateTask();
        t.AssignExecutor(Guid.NewGuid());
        t.Complete();
        return t;
    }
}