using ProjectManagement.Domain.Enums;
using ProjectManagement.Domain.Exceptions;
using ProjectManagement.Domain.Interfaces;

namespace ProjectManagement.Domain.Models;

public class WorkTask : IAuditable
{
    public const int MaxTitleLength = 400;
    public const int MaxDescriptionLength = 2000;

    private string _title = null!;
    private string? _description;
    private DateTime? _deadline;


    private WorkTask(Guid projectId, string title, string? description)
    {
        ProjectId = projectId;
        Title = title;
        Description = description;
    }

    private WorkTask() { }


    public Guid Id { get; private set; } = Guid.NewGuid();

    public string Title
    {
        get => _title;
        private set
        {
            if (string.IsNullOrWhiteSpace(value)) throw new ArgumentNullException(nameof(Title));
            if (value.Length > MaxTitleLength) throw new ArgumentOutOfRangeException(nameof(Title));
            _title = value;
        }
    }

    public string? Description
    {
        get => _description;
        private set
        {
            if (value is not null && value.Length > MaxDescriptionLength)
                throw new ArgumentOutOfRangeException(
                    nameof(Description),
                    $"Max length is {MaxDescriptionLength}  characters."
                );
            _description = value;
        }
    }

    public DateTime? Deadline
    {
        get => _deadline;
        private set
        {
            if (value.HasValue && value.Value <= DateTime.UtcNow)
                throw new ArgumentOutOfRangeException(
                    nameof(Deadline),
                    "Deadline cannot be in the past."
                );
            _deadline = value;
        }
    }

    public WorkTaskStatus Status { get; private set; } = WorkTaskStatus.Active;

    public Guid ProjectId { get; private set; }
    public Guid? ExecutorId { get; private set; }


    public static WorkTask Create(Guid projectId, string title, string? description, DateTime? deadline)
    {
        var workTask = new WorkTask(projectId, title, description);
        workTask.SetDeadline(deadline);
        return workTask;
    }

    internal void Complete()
    {
        if (Status == WorkTaskStatus.Completed) return;
        if (ExecutorId is null) throw new DomainRuleException("The task cannot be completed without an executor.");
        Status = WorkTaskStatus.Completed;
    }

    internal void Rename(string newTitle)
    {
        EnsureEditable();
        Title = newTitle;
    }

    internal void ChangeDescription(string? newDescription)
    {
        EnsureEditable();
        Description = newDescription;
    }

    internal void SetDeadline(DateTime? deadline)
    {
        EnsureEditable();
        Deadline = deadline;
    }

    internal void AssignExecutor(Guid userId)
    {
        EnsureEditable();
        ExecutorId = userId;
    }

    internal void UnassignExecutor()
    {
        ExecutorId = null;
    }

    private void EnsureEditable()
    {
        if (Status == WorkTaskStatus.Completed) throw new DomainRuleException("Cannot change completed task.");
    }
}