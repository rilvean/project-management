using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ProjectManagement.Api.Features.Projects.Shared;
using ProjectManagement.Api.Features.Shared;

namespace ProjectManagement.Api.Features.Projects.GetPage;

public static class Endpoint
{
    public static RouteGroupBuilder MapGetProjectsPage(this RouteGroupBuilder group)
    {
        group.MapGet(string.Empty, Handle);
        return group;
    }

    private static async Task<Ok<PagedResponse<ProjectResponse>>> Handle(
        [AsParameters] GetProjectsPageQuery query,
        [FromServices] ISender sender,
        CancellationToken ct)
    {
        var response = await sender.Send(query, ct);
        return TypedResults.Ok(response);
    }
}