using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ProjectManagement.Api.Features.WorkTasks.AssignExecutor;

namespace ProjectManagement.Api.Features.WorkTasks.UnassignExecutor;

public static class Endpoint
{
    public static RouteGroupBuilder MapUnassignWorkTaskExecutor(this RouteGroupBuilder group)
    {
        group.MapPost("{id:guid}/unassign-executor", Handle);
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

        var command = new UnassignWorkTaskExecutorCommand(id, actorId);
        await sender.Send(command, ct);
        return TypedResults.NoContent();
    }
}