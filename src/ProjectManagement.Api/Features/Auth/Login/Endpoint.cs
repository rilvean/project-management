using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ProjectManagement.Api.Features.Auth.Login;

public static class Endpoint
{
    public static RouteGroupBuilder MapLogin(this RouteGroupBuilder group)
    {
        group.MapPost("login",
            async (
                [FromBody] LoginCommand command,
                [FromServices] ISender sender,
                CancellationToken ct) =>
            {
                var result = await sender.Send(command, ct);
                return TypedResults.Ok(result);
            });

        return group;
    }
}