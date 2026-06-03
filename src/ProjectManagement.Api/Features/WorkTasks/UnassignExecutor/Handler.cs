using MediatR;
using Microsoft.EntityFrameworkCore;
using ProjectManagement.Domain.Services;
using ProjectManagement.Infrastructure.Persistence;

namespace ProjectManagement.Api.Features.WorkTasks.UnassignExecutor;

public sealed class Handler(WriteDbContext db)
    : IRequestHandler<UnassignWorkTaskExecutorCommand>
{
    public async Task Handle(UnassignWorkTaskExecutorCommand request, CancellationToken ct)
    {
        var project = await db.Projects
            .Include(x => x.WorkTasks)
            .FirstOrDefaultAsync(x => x.WorkTasks.Any(wt => wt.Id == request.WorkTaskId), ct);

        if (project is null)
            throw new("Project with this task not found");

        var actor = await db.Users.FindAsync([request.ActorId], ct)
            ?? throw new Exception("Actor not found");

        if (!ProjectPolicies.CanEdit(project, actor))
        {
            throw new("User do not have permission to delete this task");
        }

        project.UnassignWorkTaskExecutor(request.WorkTaskId);

        await db.SaveChangesAsync(ct);
    }
}