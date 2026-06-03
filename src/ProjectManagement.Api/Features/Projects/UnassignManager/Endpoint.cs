using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ProjectManagement.Api.Shared;

namespace ProjectManagement.Api.Features.Projects.UnassignManager;

public static class Endpoint
{
    public static RouteGroupBuilder MapUnassignProjectManager(this RouteGroupBuilder group)
    {
        group.MapPost("{id:guid}/unassign-manager", Handle)
            .RequireAuthorization(AuthorizationPolicies.SupervisorOnly);

        return group;
    }

    private static async Task<Results<NoContent, UnauthorizedHttpResult>> Handle(
        [FromRoute] Guid id,
        [FromServices] ISender sender,
        HttpContext context,
        CancellationToken ct)
    {
        var command = new UnassignProjectManagerCommand(id);
        await sender.Send(command, ct);
        return TypedResults.NoContent();
    }
}