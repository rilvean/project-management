using ProjectManagement.Api.Features.Auth;

namespace ProjectManagement.Api.Features;

public static class FeaturesEndpoints
{
    public static IEndpointRouteBuilder MapFeaturesEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapAuthEndpoints();

        return app;
    }
}