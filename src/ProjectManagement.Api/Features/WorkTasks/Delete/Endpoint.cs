using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace ProjectManagement.Api.Features.WorkTasks.Delete;

public static class Endpoint
{
    public static RouteGroupBuilder MapDeleteWorkTask(this RouteGroupBuilder group)
    {
        group.MapDelete("{id:guid}", Handle);
        return group;
    }

    private static async Task<Results<NoContent, UnauthorizedHttpResult>> Handle(
        [FromRoute] Guid id,
        [FromServices] ISender sender,
        HttpContext context,
        CancellationToken ct)
    {
        if (!Guid.TryParse(context.User.FindFirstValue(ClaimTypes.NameIdentifier), out var actorId))
        {
            return TypedResults.Unauthorized();
        }

        var command = new DeleteWorkTaskCommand(id, actorId);
        await sender.Send(command, ct);
        return TypedResults.NoContent();
    }
}