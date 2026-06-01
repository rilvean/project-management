using ProjectManagement.Api.Features.Projects.Create;
using ProjectManagement.Api.Features.Projects.Delete;
using ProjectManagement.Api.Features.Projects.GetById;
using ProjectManagement.Api.Features.Projects.GetExecutors;
using ProjectManagement.Api.Features.Projects.GetPage;
using ProjectManagement.Api.Features.Projects.GetWorkTasks;

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

        return app;
    }
}