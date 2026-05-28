using ProjectManagement.Api.Features.Projects.GetById;

namespace ProjectManagement.Api.Features.Projects;

public static class ProjectsEndpoints
{
    public static IEndpointRouteBuilder MapProjectsEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/projects").WithTags("Projects");

        group.MapGetProjectById();

        return app;
    }
}