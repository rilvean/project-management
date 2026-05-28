using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ProjectManagement.Api.Features.Users.GetPage;

public static class Endpoint
{
    public static RouteGroupBuilder MapGetUsersPage(this RouteGroupBuilder group)
    {
        group.MapGet("get-page",
            async (
                [AsParameters] GetUsersPageQuery query,
                [FromServices] ISender sender,
                CancellationToken ct) =>
            {
                var result = await sender.Send(query, ct);
                return TypedResults.Ok(result);
            });

        return group;
    }
}