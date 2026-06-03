using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace ProjectManagement.Api.Features.WorkTasks.GetMy;

public static class Endpoint
{
    public static RouteGroupBuilder MapGetMyWorkTasks(this RouteGroupBuilder group)
    {
        group.MapGet("/my", Handle);
        return group;
    }

    private static async Task<Results<NoContent, UnauthorizedHttpResult>> Handle(
        [FromServices] ISender sender,
        HttpContext context,
        CancellationToken ct)
    {
        if (!Guid.TryParse(context.User.FindFirstValue(ClaimTypes.NameIdentifier), out var userId))
        {
            return TypedResults.Unauthorized();
        }

        var command = new GetMyWorkTasksQuery(userId);
        await sender.Send(command, ct);
        return TypedResults.NoContent();
    }
}