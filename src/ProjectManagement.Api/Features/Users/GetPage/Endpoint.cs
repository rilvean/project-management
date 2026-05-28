using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ProjectManagement.Api.Features.Shared;
using ProjectManagement.Api.Features.Users.Shared;

namespace ProjectManagement.Api.Features.Users.GetPage;

public static class Endpoint
{
    public static RouteGroupBuilder MapGetUsersPage(this RouteGroupBuilder group)
    {
        group.MapGet("get-page", Handle);
        return group;
    }

    private static async Task<Ok<PagedResponse<UserResponse>>> Handle(
        [AsParameters] GetUsersPageQuery query,
        [FromServices] ISender sender,
        CancellationToken ct)
    {
        var response = await sender.Send(query, ct);
        return TypedResults.Ok(response);
    }
}