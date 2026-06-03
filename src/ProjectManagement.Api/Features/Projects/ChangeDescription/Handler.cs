using MediatR;
using Microsoft.EntityFrameworkCore;
using ProjectManagement.Domain.Services;
using ProjectManagement.Infrastructure.Persistence;

namespace ProjectManagement.Api.Features.Projects.ChangeDescription;

public sealed class Handler(WriteDbContext db)
    : IRequestHandler<ChangeProjectDescriptionCommand>
{
    public async Task Handle(ChangeProjectDescriptionCommand request, CancellationToken ct)
    {
        var project = await db.Projects
            .Where(x => x.Id == request.ProjectId)
            .FirstOrDefaultAsync(ct);

        if (project is null)
            throw new("Project not found");

        var actor = await db.Users.FindAsync([request.ActorId], ct)
            ?? throw new Exception("Actor not found");

        if (!ProjectPolicies.CanEdit(project, actor))
        {
            throw new("User do not have permission to edit this project");
        }

        project.ChangeDescription(request.Description);

        await db.SaveChangesAsync(ct);
    }
}