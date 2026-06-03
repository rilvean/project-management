using MediatR;
using Microsoft.EntityFrameworkCore;
using ProjectManagement.Api.Features.Shared;
using ProjectManagement.Infrastructure.Persistence;

namespace ProjectManagement.Api.Features.Projects.GetWorkTasks;

public sealed class Handler(ReadDbContext db)
    : IRequestHandler<GetProjectWorkTasksQuery, List<WorkTaskResponse>>
{
    public async Task<List<WorkTaskResponse>> Handle(GetProjectWorkTasksQuery request, CancellationToken ct)
    {
        var projectExists = await db.Projects
            .AnyAsync(x => x.Id == request.ProjectId, ct);

        if (!projectExists)
            throw new("Project not found");

        return await db.WorkTasks
            .Where(x => x.ProjectId == request.ProjectId)
            .Select(x => new WorkTaskResponse(
                x.Id,
                x.ProjectId,
                x.ExecutorId,
                x.Title,
                x.Description,
                x.Deadline,
                x.Status
            ))
            .ToListAsync(ct);
    }
}