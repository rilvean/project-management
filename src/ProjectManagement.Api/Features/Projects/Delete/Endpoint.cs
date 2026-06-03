using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ProjectManagement.Api.Shared;

namespace ProjectManagement.Api.Features.Projects.Delete;

public static class Endpoint
{
    public static RouteGroupBuilder MapDeleteProject(this RouteGroupBuilder group)
    {
        group.MapDelete("{id:guid}", Handle)
            .RequireAuthorization(AuthorizationPolicies.SupervisorOnly);

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

        var command = new DeleteProjectCommand(id, actorId);
        await sender.Send(command, ct);
        return TypedResults.NoContent();
    }
}