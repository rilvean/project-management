using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ProjectManagement.Api.Features.Users.Delete;

public static class Endpoint
{
    public static RouteGroupBuilder MapDeleteUser(this RouteGroupBuilder group)
    {
        group.MapDelete("{id:guid}",
            async (
                [FromRoute] Guid id,
                [FromServices] ISender sender,
                CancellationToken ct) =>
            {
                var command = new DeleteUserCommand(id);
                await sender.Send(command, ct);
                return TypedResults.NoContent();
            });

        return group;
    }
}