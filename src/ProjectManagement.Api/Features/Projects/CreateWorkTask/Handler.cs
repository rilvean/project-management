using MediatR;
using ProjectManagement.Infrastructure.Persistence;

namespace ProjectManagement.Api.Features.Projects.CreateWorkTask;

public class Handler(WriteDbContext db)
    : IRequestHandler<CreateWorkTaskCommand, CreateWorkTaskResponse>
{
    public async Task<CreateWorkTaskResponse> Handle(CreateWorkTaskCommand request, CancellationToken ct)
    {
        var project = await db.Projects.FindAsync([request.ProjectId], ct);

        if (project is null)
            throw new("Project not found");

        var workTask = project.CreateWorkTask(
            request.Title,
            request.Description,
            request.Deadline
        );

        await db.SaveChangesAsync(ct);

        return new CreateWorkTaskResponse(workTask.Id);
    }
}