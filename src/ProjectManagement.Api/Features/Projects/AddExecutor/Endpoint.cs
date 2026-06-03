using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ProjectManagement.Api.Features.Projects.AssignManager;

namespace ProjectManagement.Api.Features.Projects.AddExecutor;

public static class Endpoint
{
    public static RouteGroupBuilder MapAddProjectExecutor(this RouteGroupBuilder group)
    {
        group.MapPost("{id:guid}/add-executor", Handle);
        return group;
    }

    private static async Task<Results<NoContent, UnauthorizedHttpResult>> Handle(
        [FromRoute] Guid id,
        [FromBody] AddProjectExecutorRequest request,
        [FromServices] ISender sender,
        HttpContext context,
        CancellationToken ct)
    {
        if (!Guid.TryParse(context.User.FindFirstValue(ClaimTypes.NameIdentifier), out var actorId))
        {
            return TypedResults.Unauthorized();
        }

        var command = new AddProjectExecutorCommand(id, request.ExecutorId, actorId);
        await sender.Send(command, ct);
        return TypedResults.NoContent();
    }
}