using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ProjectManagement.Api.Features.Users.ChangeRole;

public static class Endpoint
{
    public static RouteGroupBuilder MapChangeRole(this RouteGroupBuilder group)
    {
        group.MapPost("{id:guid}/change-role",
            async (
                [FromRoute] Guid id,
                [FromBody] ChangeRoleRequest request,
                [FromServices] ISender sender,
                CancellationToken ct) =>
            {
                var command = new ChangeRoleCommand(id, request.Role);
                await sender.Send(command, ct);
                return TypedResults.NoContent();
            });

        return group;
    }
}