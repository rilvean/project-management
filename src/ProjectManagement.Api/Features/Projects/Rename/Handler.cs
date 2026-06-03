using MediatR;
using Microsoft.EntityFrameworkCore;
using ProjectManagement.Domain.Services;
using ProjectManagement.Infrastructure.Persistence;

namespace ProjectManagement.Api.Features.Projects.Rename;

public sealed class Handler(WriteDbContext db)
    : IRequestHandler<RenameProjectCommand>
{
    public async Task Handle(RenameProjectCommand request, CancellationToken ct)
    {
        var project = await db.Projects
            .Where(x => x.Id == request.ProjectId)
            .FirstOrDefaultAsync(ct);

        if (project is null)
            throw new("Project not found");

        if (!ProjectPolicies.CanEdit(project, request.ActorId))
        {
            throw new("User do not have permission to edit this project");
        }

        project.Rename(request.Title);

        await db.SaveChangesAsync(ct);
    }
}