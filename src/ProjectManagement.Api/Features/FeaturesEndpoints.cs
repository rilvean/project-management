using ProjectManagement.Api.Features.Auth;
using ProjectManagement.Api.Features.Projects;
using ProjectManagement.Api.Features.Users;
using ProjectManagement.Api.Features.WorkTasks;

namespace ProjectManagement.Api.Features;

public static class FeaturesEndpoints
{
    public static IEndpointRouteBuilder MapFeaturesEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapAuthEndpoints();
        app.MapUserEndpoints();
        app.MapProjectEndpoints();
        app.MapWorkTaskEndpoints();

        return app;
    }
}