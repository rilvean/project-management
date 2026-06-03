using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ProjectManagement.Api.Features.Projects.AddExecutor;

namespace ProjectManagement.Api.Features.Projects.RemoveExecutor;

public static class Endpoint
{
    public static RouteGroupBuilder MapRemoveProjectExecutor(this RouteGroupBuilder group)
    {
        group.MapPost("{id:guid}/remove-executor", Handle);
        return group;
    }

    private static async Task<Results<NoContent, UnauthorizedHttpResult>> Handle(
        [FromRoute] Guid id,
        [FromBody] RemoveProjectExecutorRequest request,
        [FromServices] ISender sender,
        HttpContext context,
        CancellationToken ct)
    {
        if (!Guid.TryParse(context.User.FindFirstValue(ClaimTypes.NameIdentifier), out var actorId))
        {
            return TypedResults.Unauthorized();
        }

        var command = new RemoveProjectExecutorCommand(id, request.ExecutorId, actorId);
        await sender.Send(command, ct);
        return TypedResults.NoContent();
    }
}