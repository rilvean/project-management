using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ProjectManagement.Api.Features.Users.Rename;

public static class Endpoint
{
    public static RouteGroupBuilder MapRenameUser(this RouteGroupBuilder group)
    {
        group.MapPost("{id:guid}/rename",
            async (
                [FromRoute] Guid id,
                [FromBody] RenameUserRequest request,
                [FromServices] ISender sender,
                CancellationToken ct) =>
            {
                var command = new RenameUserCommand(id, request.Name);
                await sender.Send(command, ct);
                return TypedResults.NoContent();
            });

        return group;
    }
}