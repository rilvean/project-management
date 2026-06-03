using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace ProjectManagement.Api.Features.WorkTasks.ChangeDescription;

public static class Endpoint
{
    public static RouteGroupBuilder MapChangeWorkTaskDescription(this RouteGroupBuilder group)
    {
        group.MapPost("{id:guid}/change-description", Handle);
        return group;
    }

    private static async Task<Results<NoContent, UnauthorizedHttpResult>> Handle(
        [FromRoute] Guid id,
        [FromBody] ChangeWorkTaskDescriptionRequest request,
        [FromServices] ISender sender,
        HttpContext context,
        CancellationToken ct)
    {
        if (!Guid.TryParse(context.User.FindFirstValue(ClaimTypes.NameIdentifier), out var actorId))
        {
            return TypedResults.Unauthorized();
        }

        var command = new ChangeWorkTaskDescriptionCommand(id, request.Description, actorId);
        await sender.Send(command, ct);
        return TypedResults.NoContent();
    }
}