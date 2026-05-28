using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace ProjectManagement.Api.Features.Users.ChangeRole;

public static class Endpoint
{
    public static RouteGroupBuilder MapChangeRole(this RouteGroupBuilder group)
    {
        group.MapPost("{id:guid}/change-role", Handle);
        return group;
    }

    private static async Task<NoContent> Handle(
        [FromRoute] Guid id,
        [FromBody] ChangeRoleRequest request,
        [FromServices] ISender sender,
        CancellationToken ct)
    {
        var command = new ChangeRoleCommand(id, request.Role);
        await sender.Send(command, ct);
        return TypedResults.NoContent();
    }
}