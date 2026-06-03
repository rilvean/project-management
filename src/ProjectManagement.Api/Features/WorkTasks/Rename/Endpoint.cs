using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ProjectManagement.Api.Features.WorkTasks.ChangeDescription;

namespace ProjectManagement.Api.Features.WorkTasks.Rename;

public static class Endpoint
{
    public static RouteGroupBuilder MapRenameWorkTask(this RouteGroupBuilder group)
    {
        group.MapPost("{id:guid}/rename", Handle);
        return group;
    }

    private static async Task<Results<NoContent, UnauthorizedHttpResult>> Handle(
        [FromRoute] Guid id,
        [FromBody] RenameWorkTaskRequest request,
        [FromServices] ISender sender,
        HttpContext context,
        CancellationToken ct)
    {
        if (!Guid.TryParse(context.User.FindFirstValue(ClaimTypes.NameIdentifier), out var actorId))
        {
            return TypedResults.Unauthorized();
        }

        var command = new RenameWorkTaskCommand(id, request.Title, actorId);
        await sender.Send(command, ct);
        return TypedResults.NoContent();
    }
}