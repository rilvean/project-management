using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace ProjectManagement.Api.Features.Projects.Create;

public static class Endpoint
{
    public static RouteGroupBuilder MapCreateProject(this RouteGroupBuilder group)
    {
        group.MapPost(string.Empty, Handle);
        return group;
    }

    private static async Task<Ok<CreateProjectResponse>> Handle(
        [FromBody] CreateProjectCommand command,
        [FromServices] ISender sender,
        CancellationToken ct)
    {
        var response = await sender.Send(command, ct);
        return TypedResults.Ok(response);
    }
}