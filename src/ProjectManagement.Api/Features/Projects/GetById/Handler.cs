using MediatR;
using ProjectManagement.Api.Features.Projects.Shared;
using ProjectManagement.Infrastructure.Persistence;

namespace ProjectManagement.Api.Features.Projects.GetById;

public class Handler(ProjectManagementDbContext db)
    : IRequestHandler<GetProjectByIdQuery, ProjectResponse>
{
    public async Task<ProjectResponse> Handle(GetProjectByIdQuery request, CancellationToken ct)
    {
        var project = await db.Projects.FindAsync(request.ProjectId, ct);

        if (project is null)
            throw new("Project not found");

        return new ProjectResponse(
            project.Id,
            project.ManagerId,
            project.Title,
            project.Description,
            project.Priority,
            project.Status
        );
    }
}