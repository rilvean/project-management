using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace ProjectManagement.Api.Features.Projects.CreateWorkTask;

public static class Endpoint
{
    public static RouteGroupBuilder MapCreateWorkTask(this RouteGroupBuilder group)
    {
        group.MapPost("{id:guid}/create-work-task", Handle);
        return group;
    }

    private static async Task<Ok<CreateWorkTaskResponse>> Handle(
        [FromRoute] Guid id,
        [FromBody] CreateWorkTaskRequest request,
        [FromServices] ISender sender,
        CancellationToken ct)
    {
        var command = new CreateWorkTaskCommand(
            id,
            request.Title,
            request.Description,
            request.Deadline
        );
        var response = await sender.Send(command);
        return TypedResults.Ok(response);
    }
}