using ProjectManagement.Domain.Enums;
using ProjectManagement.Domain.Exceptions;
using ProjectManagement.Domain.Interfaces;

namespace ProjectManagement.Domain.Models;

public class Project : IAuditable
{
    public const int MaxTitleLength = 400;
    public const int MaxDescriptionLength = 2000;

    private string _title = null!;
    private string? _description;

    private readonly List<WorkTask> _workTasks = [];
    private readonly List<ProjectExecutor> _executors = [];


    public Project(string title, string? description, ProjectPriority priority)
    {
        Title = title;
        Description = description;
        Priority = priority;
    }

    private Project() { }


    public Guid Id { get; private set; } = Guid.NewGuid();

    public Guid? ManagerId { get; private set; }

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
                    $"Max length is {MaxDescriptionLength} characters."
                );

            _description = value;
        }
    }

    public ProjectPriority Priority { get; private set; } = ProjectPriority.Medium;
    public ProjectStatus Status { get; private set; } = ProjectStatus.Active;

    public IReadOnlyCollection<WorkTask> WorkTasks => _workTasks;
    public IReadOnlyCollection<ProjectExecutor> Executors => _executors;


    public void ChangeTitle(string newTitle)
    {
        EnsureEditable();
        Title = newTitle;
    }

    public void ChangeDescription(string? newDescription)
    {
        EnsureEditable();
        Description = newDescription;
    }

    public void AssignManager(Guid userId)
    {
        EnsureEditable();
        ManagerId = userId;
    }

    public void UnassignManager()
    {
        ManagerId = null;
    }

    public void AddExecutor(Guid userId)
    {
        EnsureEditable();

        if (ManagerId == userId) throw new DomainRuleException("Manager cannot be project executor.");
        if (_executors.All(x => x.UserId != userId))
        {
            _executors.Add(new ProjectExecutor(Id, userId));
        }
    }

    public void RemoveExecutor(Guid userId)
    {
        if (_workTasks.Any(x => x.ExecutorId == userId && x.Status != WorkTaskStatus.Completed))
        {
            throw new DomainRuleException("This executor has active tasks.");
        }

        foreach (var task in _workTasks.Where(x => x.ExecutorId == userId))
        {
            task.UnassignExecutor();
        }

        _executors.RemoveAll(x => x.UserId == userId);
    }

    public void Complete()
    {
        EnsureEditable();

        if (_workTasks.Any(x => x.Status != WorkTaskStatus.Completed))
        {
            throw new DomainRuleException("The project contains uncompleted tasks.");
        }

        Status = ProjectStatus.Completed;
    }

    public void CreateWorkTask(string title, string? description, DateTime? deadline)
    {
        EnsureEditable();

        var workTask = WorkTask.Create(Id, title, description, deadline);

        _workTasks.Add(workTask);
    }

    public void RemoveWorkTask(Guid workTaskId)
    {
        EnsureEditable();

        var workTask = GetWorkTask(workTaskId);
        _workTasks.Remove(workTask);
    }

    public void AssignWorkTaskExecutor(Guid workTaskId, Guid userId)
    {
        EnsureEditable();

        var workTask = GetWorkTask(workTaskId);

        if (_executors.All(x => x.UserId != userId))
            throw new DomainRuleException("This user is not a project executor.");

        workTask.AssignExecutor(userId);
    }

    public void UnassignWorkTaskExecutor(Guid workTaskId)
    {
        var workTask = GetWorkTask(workTaskId);
        workTask.UnassignExecutor();
    }

    public void RenameWorkTask(Guid workTaskId, string newTitle)
    {
        EnsureEditable();

        var workTask = GetWorkTask(workTaskId);
        workTask.Rename(newTitle);
    }

    public void ChangeWorkTaskDescription(Guid workTaskId, string newDescription)
    {
        EnsureEditable();

        var workTask = GetWorkTask(workTaskId);
        workTask.ChangeDescription(newDescription);
    }

    public void ChangeWorkTaskDeadline(Guid workTaskId, DateTime? deadline)
    {
        EnsureEditable();

        var workTask = GetWorkTask(workTaskId);
        workTask.SetDeadline(deadline);
    }

    public void CompleteWorkTask(Guid workTaskId)
    {
        EnsureEditable();

        var workTask = GetWorkTask(workTaskId);
        workTask.Complete();
    }

    private WorkTask GetWorkTask(Guid workTaskId)
    {
        return _workTasks.FirstOrDefault(x => x.Id == workTaskId)
            ?? throw new DomainRuleException("This task is not in the project.");
    }

    private void EnsureEditable()
    {
        if (Status == ProjectStatus.Completed) throw new DomainRuleException("Cannot change completed project.");
    }
}