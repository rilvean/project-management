using MediatR;
using Microsoft.EntityFrameworkCore;
using ProjectManagement.Api.Features.Projects.AddExecutor;
using ProjectManagement.Domain.Services;
using ProjectManagement.Infrastructure.Persistence;

namespace ProjectManagement.Api.Features.Projects.RemoveExecutor;

public class Handler(WriteDbContext db)
    : IRequestHandler<RemoveProjectExecutorCommand>
{
    public async Task Handle(RemoveProjectExecutorCommand request, CancellationToken ct)
    {
        var project = await db.Projects
            .Include(x => x.Executors)
            .Where(x => x.Id == request.ProjectId)
            .FirstOrDefaultAsync(ct);

        if (project is null)
            throw new("Project not found");

        if (!ProjectPolicies.CanEdit(project, request.ActorId))
        {
            throw new("User do not have permission to edit this project");
        }

        project.RemoveExecutor(request.ExecutorId);

        await db.SaveChangesAsync(ct);
    }
}