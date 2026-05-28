using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ProjectManagement.Api.Features.Projects.GetById;

public static class Endpoint
{
    public static RouteGroupBuilder MapGetProjectById(this RouteGroupBuilder group)
    {
        group.MapGet("{id:guid}",
            async (
                [FromRoute] Guid id,
                [FromServices] ISender sender,
                CancellationToken ct) =>
            {
                var query = new GetProjectByIdQuery(id);
                var result = await sender.Send(query, ct);
                return TypedResults.Ok(result);
            });

        return group;
    }
}