using ProjectManagement.Api.Features.Projects.AddExecutor;
using ProjectManagement.Api.Features.Projects.AssignManager;
using ProjectManagement.Api.Features.Projects.ChangeDescription;
using ProjectManagement.Api.Features.Projects.Complete;
using ProjectManagement.Api.Features.Projects.Create;
using ProjectManagement.Api.Features.Projects.CreateWorkTask;
using ProjectManagement.Api.Features.Projects.Delete;
using ProjectManagement.Api.Features.Projects.GetById;
using ProjectManagement.Api.Features.Projects.GetExecutors;
using ProjectManagement.Api.Features.Projects.GetPage;
using ProjectManagement.Api.Features.Projects.GetWorkTasks;
using ProjectManagement.Api.Features.Projects.RemoveExecutor;
using ProjectManagement.Api.Features.Projects.Rename;
using ProjectManagement.Api.Features.Projects.UnassignManager;

namespace ProjectManagement.Api.Features.Projects;

public static class ProjectEndpoints
{
    public static IEndpointRouteBuilder MapProjectEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/projects").WithTags("Projects");

        group.MapGetProjectById();
        group.MapGetProjectsPage();
        group.MapDeleteProject();
        group.MapGetExecutors();
        group.MapCreateProject();
        group.MapGetProjectWorkTasks();
        group.MapCompleteProject();
        group.MapAssignProjectManager();
        group.MapUnassignProjectManager();
        group.MapAddProjectExecutor();
        group.MapRemoveProjectExecutor();
        group.MapChangeProjectDescription();
        group.MapRenameProject();
        group.MapCreateWorkTask();

        return app;
    }
}