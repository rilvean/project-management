using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace ProjectManagement.Api.Features.Auth.Login;

public static class Endpoint
{
    public static RouteGroupBuilder MapLogin(this RouteGroupBuilder group)
    {
        group.MapPost("login", Handle);
        return group;
    }

    private static async Task<Ok<LoginResponse>> Handle(
        [FromBody] LoginCommand command,
        [FromServices] ISender sender,
        CancellationToken ct)
    {
        var response = await sender.Send(command, ct);
        return TypedResults.Ok(response);
    }
}