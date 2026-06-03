using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ProjectManagement.Api.Features.Shared;

namespace ProjectManagement.Api.Features.Projects.GetExecutors;

public static class Endpoint
{
    public static RouteGroupBuilder MapGetExecutors(this RouteGroupBuilder group)
    {
        group.MapGet("{id:guid}/executors", Handle);
        return group;
    }

    private static async Task<Ok<List<UserResponse>>> Handle(
        [FromRoute] Guid id,
        [FromServices] ISender sender,
        CancellationToken ct)
    {
        var query = new GetExecutorsQuery(id);
        var response = await sender.Send(query, ct);
        return TypedResults.Ok(response);
    }
}