using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace ProjectManagement.Api.Features.Projects.Delete;

public static class Endpoint
{
    public static RouteGroupBuilder MapDeleteProject(this RouteGroupBuilder group)
    {
        group.MapDelete("{id:guid}", Handle);
        return group;
    }

    private static async Task<NoContent> Handle(
        [FromRoute] Guid id,
        [FromServices] ISender sender,
        CancellationToken ct)
    {
        var command = new DeleteProjectCommand(id);
        await sender.Send(command, ct);
        return TypedResults.NoContent();
    }
}