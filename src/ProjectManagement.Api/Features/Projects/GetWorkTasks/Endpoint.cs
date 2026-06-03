using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ProjectManagement.Api.Features.Shared;

namespace ProjectManagement.Api.Features.Projects.GetWorkTasks;

public static class Endpoint
{
    public static RouteGroupBuilder MapGetProjectWorkTasks(this RouteGroupBuilder group)
    {
        group.MapGet("{id:guid}/work-tasks", Handle);
        return group;
    }

    private static async Task<Ok<List<WorkTaskResponse>>> Handle(
        [FromRoute] Guid id,
        [FromServices] ISender sender,
        CancellationToken ct)
    {
        var query = new GetProjectWorkTasksQuery(id);
        var response = await sender.Send(query, ct);
        return TypedResults.Ok(response);
    }
}