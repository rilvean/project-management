using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace ProjectManagement.Api.Features.Users.Rename;

public static class Endpoint
{
    public static RouteGroupBuilder MapRenameUser(this RouteGroupBuilder group)
    {
        group.MapPost("{id:guid}/rename", Handle);
        return group;
    }

    private static async Task<NoContent> Handle(
        [FromRoute] Guid id,
        [FromBody] RenameUserRequest request,
        [FromServices] ISender sender,
        CancellationToken ct)
    {
        var command = new RenameUserCommand(id, request.Name);
        await sender.Send(command, ct);
        return TypedResults.NoContent();
    }
}