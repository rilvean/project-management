using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ProjectManagement.Api.Features.Projects.Shared;

namespace ProjectManagement.Api.Features.Projects.GetById;

public static class Endpoint
{
    public static RouteGroupBuilder MapGetProjectById(this RouteGroupBuilder group)
    {
        group.MapGet("{id:guid}", Handle);
        return group;
    }

    private static async Task<Ok<ProjectResponse>> Handle(
        [FromRoute] Guid id,
        [FromServices] ISender sender,
        CancellationToken ct)
    {
        var query = new GetProjectByIdQuery(id);
        var response = await sender.Send(query, ct);
        return TypedResults.Ok(response);
    }
}