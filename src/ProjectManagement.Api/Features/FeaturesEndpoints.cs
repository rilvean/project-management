using ProjectManagement.Api.Features.Auth;
using ProjectManagement.Api.Features.Projects;
using ProjectManagement.Api.Features.Users;

namespace ProjectManagement.Api.Features;

public static class FeaturesEndpoints
{
    public static IEndpointRouteBuilder MapFeaturesEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapAuthEndpoints();
        app.MapUsersEndpoints();
        app.MapProjectsEndpoints();

        return app;
    }
}