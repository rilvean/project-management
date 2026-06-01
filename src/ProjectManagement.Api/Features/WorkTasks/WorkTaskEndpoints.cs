using ProjectManagement.Api.Features.WorkTasks.GetById;

namespace ProjectManagement.Api.Features.WorkTasks;

public static class WorkTaskEndpoints
{
    public static IEndpointRouteBuilder MapWorkTaskEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/work-tasks").WithTags("WorkTasks");

        group.MapGetWorkTaskById();
        
        return app;
    }
}