using MediatR;
using ProjectManagement.Infrastructure.Persistence;

namespace ProjectManagement.Api.Features.Projects.UnassignManager;

public sealed class Handler(WriteDbContext db)
    : IRequestHandler<UnassignProjectManagerCommand>
{
    public async Task Handle(UnassignProjectManagerCommand request, CancellationToken ct)
    {
        var project = await db.Projects.FindAsync([request.ProjectId], ct);

        if (project is null)
            throw new("Project not found");

        project.UnassignManager();

        await db.SaveChangesAsync(ct);
    }
}