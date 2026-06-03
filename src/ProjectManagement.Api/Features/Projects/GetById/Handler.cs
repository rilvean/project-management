using MediatR;
using Microsoft.EntityFrameworkCore;
using ProjectManagement.Api.Features.Projects.Shared;
using ProjectManagement.Infrastructure.Persistence;

namespace ProjectManagement.Api.Features.Projects.GetById;

public class Handler(ReadDbContext db)
    : IRequestHandler<GetProjectByIdQuery, ProjectResponse>
{
    public async Task<ProjectResponse> Handle(GetProjectByIdQuery request, CancellationToken ct)
    {
        var project = await db.Projects
            .Where(p => p.Id == request.ProjectId)
            .Select(x => new ProjectResponse(
                x.Id,
                x.ManagerId,
                x.Title,
                x.Description,
                x.Priority,
                x.Status
            ))
            .FirstOrDefaultAsync(ct);

        if (project is null)
            throw new("Project not found");

        return project;
    }
}