using ProjectManagement.Api.Features.WorkTasks.AssignExecutor;
using ProjectManagement.Api.Features.WorkTasks.ChangeDeadline;
using ProjectManagement.Api.Features.WorkTasks.ChangeDescription;
using ProjectManagement.Api.Features.WorkTasks.Complete;
using ProjectManagement.Api.Features.WorkTasks.Delete;
using ProjectManagement.Api.Features.WorkTasks.GetById;
using ProjectManagement.Api.Features.WorkTasks.GetMy;
using ProjectManagement.Api.Features.WorkTasks.Rename;
using ProjectManagement.Api.Features.WorkTasks.UnassignExecutor;
using ProjectManagement.Api.Shared;

namespace ProjectManagement.Api.Features.WorkTasks;

public static class WorkTaskEndpoints
{
    public static IEndpointRouteBuilder MapWorkTaskEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/work-tasks").WithTags("WorkTasks")
            .RequireAuthorization(AuthorizationPolicies.ProjectManagerOrSupervisor);

        group.MapGetWorkTaskById();
        group.MapCompleteWorkTask();
        group.MapDeleteWorkTask();
        group.MapAssignWorkTaskExecutor();
        group.MapUnassignWorkTaskExecutor();
        group.MapGetMyWorkTasks();
        group.MapRenameWorkTask();
        group.MapChangeWorkTaskDescription();
        group.MapChangeWorkTaskDeadline();

        return app;
    }
}