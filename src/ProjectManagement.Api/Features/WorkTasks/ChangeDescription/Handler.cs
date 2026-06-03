using MediatR;
using Microsoft.EntityFrameworkCore;
using ProjectManagement.Api.Features.WorkTasks.ChangeDeadline;
using ProjectManagement.Domain.Services;
using ProjectManagement.Infrastructure.Persistence;

namespace ProjectManagement.Api.Features.WorkTasks.ChangeDescription;

public class Handler(WriteDbContext db)
    : IRequestHandler<ChangeWorkTaskDescriptionCommand>
{
    public async Task Handle(ChangeWorkTaskDescriptionCommand request, CancellationToken ct)
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

        project.ChangeWorkTaskDescription(request.WorkTaskId, request.Description);

        await db.SaveChangesAsync(ct);
    }
}