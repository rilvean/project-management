using ProjectManagement.Api.Features.Auth.Login;
using ProjectManagement.Api.Features.Auth.Register;

namespace ProjectManagement.Api.Features.Auth;

public static class AuthEndpoints
{
    public static IEndpointRouteBuilder MapAuthEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/auth").WithTags("Auth");

        group.MapRegister();
        group.MapLogin();

        return app;
    }
}