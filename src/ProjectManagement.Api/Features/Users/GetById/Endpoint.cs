using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ProjectManagement.Api.Features.Users.GetById;

public static class Endpoint
{
    public static RouteGroupBuilder MapGetUserById(this RouteGroupBuilder group)
    {
        group.MapGet("{id:guid}",
            async (
                [FromRoute] Guid id,
                [FromServices] ISender sender,
                CancellationToken ct) =>
            {
                var query = new GetUserByIdQuery(id);
                var result = await sender.Send(query, ct);
                return TypedResults.Ok(result);
            });

        return group;
    }
}