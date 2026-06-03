using MediatR;
using ProjectManagement.Api.Features.Projects.Complete;
using ProjectManagement.Domain.Services;
using ProjectManagement.Infrastructure.Persistence;

namespace ProjectManagement.Api.Features.Projects.AssignManager;

public class Handler(WriteDbContext db)
    : IRequestHandler<AssignProjectManagerCommand>
{
    public async Task Handle(AssignProjectManagerCommand request, CancellationToken ct)
    {
        var project = await db.Projects.FindAsync([request.ProjectId], ct);

        if (project is null)
            throw new("Project not found");

        project.AssignManager(request.ManagerId);

        await db.SaveChangesAsync(ct);
    }
}