using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace ProjectManagement.Api.Features.Users.ChangeEmail;

public static class Endpoint
{
    public static RouteGroupBuilder MapChangeEmail(this RouteGroupBuilder group)
    {
        group.MapPost("{id:guid}/change-email", Handle);
        return group;
    }

    private static async Task<NoContent> Handle(
        [FromRoute] Guid id,
        [FromBody] ChangeEmailRequest request,
        [FromServices] ISender sender,
        CancellationToken ct)
    {
        var command = new ChangeEmailCommand(id, request.Email);
        await sender.Send(command, ct);
        return TypedResults.NoContent();
    }
}