using MediatR;
using Microsoft.EntityFrameworkCore;
using ProjectManagement.Domain.Services;
using ProjectManagement.Infrastructure.Persistence;

namespace ProjectManagement.Api.Features.WorkTasks.Complete;

public sealed class Handler(WriteDbContext db)
    : IRequestHandler<CompleteWorkTaskCommand>
{
    public async Task Handle(CompleteWorkTaskCommand request, CancellationToken ct)
    {
        var project = await db.Projects
            .Include(x => x.WorkTasks)
            .FirstOrDefaultAsync(x => x.WorkTasks.Any(wt => wt.Id == request.WorkTaskId), ct);

        if (project is null)
            throw new("Project with this task not found");

        var actor = await db.Users.FindAsync([request.ActorId], ct)
            ?? throw new Exception("Actor not found");

        if (!WorkTaskPolicies.CanComplete(project, request.WorkTaskId, actor))
        {
            throw new("User do not have permission to complete this task");
        }

        project.CompleteWorkTask(request.WorkTaskId);

        await db.SaveChangesAsync(ct);
    }
}