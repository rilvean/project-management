using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProjectManagement.Domain.Enums;

namespace ProjectManagement.Api.Features.Auth.Register;

public static class Endpoint
{
    public static RouteGroupBuilder MapRegister(this RouteGroupBuilder group)
    {
        group.MapPost("register",
            async (
                [FromBody] RegisterCommand registerCommand,
                [FromServices] ISender sender,
                CancellationToken ct) =>
            {
                var userId = await sender.Send(registerCommand, ct);
                return TypedResults.Ok(userId);
            })/*.RequireAuthorization(policy => policy.RequireRole(nameof(UserRole.Admin)))*/;

        return group;
    }
}