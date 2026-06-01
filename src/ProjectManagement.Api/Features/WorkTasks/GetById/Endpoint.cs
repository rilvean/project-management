using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ProjectManagement.Api.Features.Shared;

namespace ProjectManagement.Api.Features.WorkTasks.GetById;

public static class Endpoint
{
    public static RouteGroupBuilder MapGetWorkTaskById(this RouteGroupBuilder group)
    {
        group.MapGet("{id:guid}", Handle);
        return group;
    }

    private static async Task<Ok<WorkTaskResponse>> Handle(
        [FromRoute] Guid id,
        [FromServices] ISender sender,
        CancellationToken ct)
    {
        var query = new GetWorkTaskByIdQuery(id);
        var response = await sender.Send(query, ct);
        return TypedResults.Ok(response);
    }
}