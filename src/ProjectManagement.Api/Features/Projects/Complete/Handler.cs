using MediatR;
using Microsoft.EntityFrameworkCore;
using ProjectManagement.Domain.Policies;
using ProjectManagement.Infrastructure.Persistence;

namespace ProjectManagement.Api.Features.Projects.Complete;

public sealed class Handler(WriteDbContext db)
    : IRequestHandler<CompleteProjectCommand>
{
    public async Task Handle(CompleteProjectCommand request, CancellationToken ct)
    {
        var project = await db.Projects
            .Where(p => p.Id == request.ProjectId)
            .FirstOrDefaultAsync(ct);

        if (project is null)
            throw new("Project not found");

        var actor = await db.Users.FindAsync([request.ActorId], ct)
            ?? throw new Exception("Actor not found");

        if (!ProjectPolicy.CanEdit(project, actor))
        {
            throw new("User do not have permission to edit this project");
        }

        project.Complete();

        await db.SaveChangesAsync(ct);
    }
}