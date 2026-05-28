using ProjectManagement.Api.Features.Users.ChangeEmail;
using ProjectManagement.Api.Features.Users.ChangePassword;
using ProjectManagement.Api.Features.Users.ChangeRole;
using ProjectManagement.Api.Features.Users.Delete;
using ProjectManagement.Api.Features.Users.GetById;
using ProjectManagement.Api.Features.Users.GetPage;
using ProjectManagement.Api.Features.Users.Rename;

namespace ProjectManagement.Api.Features.Users;

public static class UsersEndpoints
{
    public static IEndpointRouteBuilder MapUsersEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/users").WithTags("Users");

        group.MapGetUserById();
        group.MapGetUsersPage();
        group.MapRenameUser();
        group.MapChangeEmail();
        group.MapChangePassword();
        group.MapChangeRole();
        group.MapDeleteUser();

        return app;
    }
}