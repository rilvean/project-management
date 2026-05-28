using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ProjectManagement.Api.Features.Auth.Register;

public static class Endpoint
{
    public static RouteGroupBuilder MapRegister(this RouteGroupBuilder group)
    {
        group.MapPost("register",
            async (
                [FromBody] RegisterCommand command,
                [FromServices] ISender sender,
                CancellationToken ct) =>
            {
                var userId = await sender.Send(command, ct);
                return TypedResults.Ok(userId);
            }) /*.RequireAuthorization(policy => policy.RequireRole(nameof(UserRole.Admin)))*/;

        return group;
    }
}