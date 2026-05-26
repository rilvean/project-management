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

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public void Create_Should_Throw_When_Title_Is_Empty(string title)
    {
        Assert.Throws<ArgumentNullException>(() =>
            WorkTask.Create(
                Guid.CreateVersion7(),
                title,
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
                Guid.CreateVersion7(),
                title,
                "desc",
                DateTime.UtcNow.AddDays(1))
        );
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
    public void Complete_Should_Throw_When_No_Executor()
    {
        var task = CreateTask();

        Assert.Throws<DomainRuleException>(() => task.Complete());
    }

    [Fact]
    public void Complete_Should_Be_Idempotent()
    {
        var task = CreateTask();

        var id = Guid.CreateVersion7();

        task.AssignExecutor(id);

        task.Complete();
        task.Complete();

        Assert.Equal(WorkTaskStatus.Completed, task.Status);
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
            Guid.CreateVersion7(),
            "task",
            "desc",
            DateTime.UtcNow.AddDays(1)
        );
    }

    private static WorkTask CreateCompletedTask()
    {
        var t = CreateTask();
        t.AssignExecutor(Guid.CreateVersion7());
        t.Complete();
        return t;
    }
}