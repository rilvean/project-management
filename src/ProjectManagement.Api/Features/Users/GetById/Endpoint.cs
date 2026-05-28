using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ProjectManagement.Api.Features.Users.Shared;

namespace ProjectManagement.Api.Features.Users.GetById;

public static class Endpoint
{
    public static RouteGroupBuilder MapGetUserById(this RouteGroupBuilder group)
    {
        group.MapGet("{id:guid}", Handle);
        return group;
    }

    private static async Task<Ok<UserResponse>> Handle(
        [FromRoute] Guid id,
        [FromServices] ISender sender,
        CancellationToken ct)
    {
        var query = new GetUserByIdQuery(id);
        var response = await sender.Send(query, ct);
        return TypedResults.Ok(response);
    }
}