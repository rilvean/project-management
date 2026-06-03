using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace ProjectManagement.Api.Features.Projects.AssignManager;

public static class Endpoint
{
    public static RouteGroupBuilder MapAssignProjectManager(this RouteGroupBuilder group)
    {
        group.MapPost("{id:guid}/assign-manager", Handle);
        return group;
    }

    private static async Task<Results<NoContent, UnauthorizedHttpResult>> Handle(
        [FromRoute] Guid id,
        [FromBody] AssignProjectManagerRequest request,
        [FromServices] ISender sender,
        CancellationToken ct)
    {
        var command = new AssignProjectManagerCommand(id, request.ManagerId);
        await sender.Send(command, ct);
        return TypedResults.NoContent();
    }
}