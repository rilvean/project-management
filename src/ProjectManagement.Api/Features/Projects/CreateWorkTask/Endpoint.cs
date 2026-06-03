using System.Security.Claims;
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

    private static async Task<Results<Ok<CreateWorkTaskResponse>, UnauthorizedHttpResult>> Handle(
        [FromRoute] Guid id,
        [FromBody] CreateWorkTaskRequest request,
        [FromServices] ISender sender,
        HttpContext context,
        CancellationToken ct)
    {
        if (!Guid.TryParse(context.User.FindFirstValue(ClaimTypes.NameIdentifier), out var actorId))
        {
            return TypedResults.Unauthorized();
        }

        var command = new CreateWorkTaskCommand(
            id,
            request.Title,
            request.Description,
            request.Deadline,
            actorId
        );
        var response = await sender.Send(command);
        return TypedResults.Ok(response);
    }
}