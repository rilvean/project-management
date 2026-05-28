using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace ProjectManagement.Api.Features.Users.ChangePassword;

public static class Endpoint
{
    public static RouteGroupBuilder MapChangePassword(this RouteGroupBuilder group)
    {
        group.MapPost("{id:guid}/change-password", Handle);
        return group;
    }

    private static async Task<NoContent> Handle(
        [FromRoute] Guid id,
        [FromBody] ChangePasswordRequest request,
        [FromServices] ISender sender,
        CancellationToken ct)
    {
        var command = new ChangePasswordCommand(id, request.Password);
        await sender.Send(command, ct);
        return TypedResults.NoContent();
    }
}