using MediatR;
using Microsoft.EntityFrameworkCore;
using ProjectManagement.Domain.Services;
using ProjectManagement.Infrastructure.Persistence;

namespace ProjectManagement.Api.Features.Projects.CreateWorkTask;

public sealed class Handler(WriteDbContext db)
    : IRequestHandler<CreateWorkTaskCommand, CreateWorkTaskResponse>
{
    public async Task<CreateWorkTaskResponse> Handle(CreateWorkTaskCommand request, CancellationToken ct)
    {
        var project = await db.Projects
            .Where(p => p.Id == request.ProjectId)
            .FirstOrDefaultAsync(ct);

        if (project is null)
            throw new("Project not found");

        if (!ProjectPolicies.CanEdit(project, request.ActorId))
        {
            throw new("User do not have permission to edit this project");
        }

        var workTask = project.CreateWorkTask(
            request.Title,
            request.Description,
            request.Deadline
        );

        await db.SaveChangesAsync(ct);

        return new CreateWorkTaskResponse(workTask.Id);
    }
}