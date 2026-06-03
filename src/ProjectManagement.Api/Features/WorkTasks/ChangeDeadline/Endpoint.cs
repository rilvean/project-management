using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace ProjectManagement.Api.Features.WorkTasks.ChangeDeadline;

public static class Endpoint
{
    public static RouteGroupBuilder MapChangeWorkTaskDeadline(this RouteGroupBuilder group)
    {
        group.MapPost("{id:guid}/assign-executor", Handle);
        return group;
    }

    private static async Task<Results<NoContent, UnauthorizedHttpResult>> Handle(
        [FromRoute] Guid id,
        [FromBody] ChangeWorkTaskDeadlineRequest request,
        [FromServices] ISender sender,
        HttpContext context,
        CancellationToken ct)
    {
        if (!Guid.TryParse(context.User.FindFirstValue(ClaimTypes.NameIdentifier), out var actorId))
        {
            return TypedResults.Unauthorized();
        }

        var command = new ChangeWorkTaskDeadlineCommand(id, request.Deadline, actorId);
        await sender.Send(command, ct);
        return TypedResults.NoContent();
    }
}