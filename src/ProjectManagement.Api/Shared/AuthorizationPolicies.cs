namespace ProjectManagement.Api.Shared;

public static class AuthorizationPolicies
{
    public const string AdminOrSupervisor = nameof(AdminOrSupervisor);
    public const string CanCompleteTasks = nameof(CanCompleteTasks);
    public const string SupervisorOnly = nameof(SupervisorOnly);
    public const string ProjectManagerOrSupervisor = nameof(ProjectManagerOrSupervisor);
}