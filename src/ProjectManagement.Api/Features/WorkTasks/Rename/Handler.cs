using MediatR;
using Microsoft.EntityFrameworkCore;
using ProjectManagement.Domain.Services;
using ProjectManagement.Infrastructure.Persistence;

namespace ProjectManagement.Api.Features.WorkTasks.Rename;

public sealed class Handler(WriteDbContext db)
    : IRequestHandler<RenameWorkTaskCommand>
{
    public async Task Handle(RenameWorkTaskCommand request, CancellationToken ct)
    {
        var project = await db.Projects
            .Include(x => x.WorkTasks)
            .FirstOrDefaultAsync(x => x.WorkTasks.Any(wt => wt.Id == request.WorkTaskId), ct);

        if (project is null)
            throw new("Project with this task not found");

        if (!WorkTaskPolicies.CanEdit(project, request.ActorId))
        {
            throw new("User do not have permission to edit this task");
        }

        project.RenameWorkTask(request.WorkTaskId, request.Title);

        await db.SaveChangesAsync(ct);
    }
}