using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ProjectManagement.Domain.Enums;

namespace ProjectManagement.Api.Features.Auth.Register;

public static class Endpoint
{
    public static RouteGroupBuilder MapRegister(this RouteGroupBuilder group)
    {
        group.MapPost("register", Handle)
            .RequireAuthorization(nameof(UserRole.Admin));

        return group;
    }

    private static async Task<Ok<RegisterResponse>> Handle(
        [FromBody] RegisterCommand command,
        [FromServices] ISender sender,
        CancellationToken ct)
    {
        var response = await sender.Send(command, ct);
        return TypedResults.Ok(response);
    }
}