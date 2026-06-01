using MediatR;
using Microsoft.EntityFrameworkCore;
using ProjectManagement.Api.Features.Shared;
using ProjectManagement.Infrastructure.Persistence;

namespace ProjectManagement.Api.Features.Projects.GetWorkTasks;

public class Handler(ProjectManagementDbContext db)
    : IRequestHandler<GetProjectWorkTasksQuery, List<WorkTaskResponse>>
{
    public async Task<List<WorkTaskResponse>> Handle(GetProjectWorkTasksQuery request, CancellationToken ct)
    {
        var projectExists = await db.Projects
            .AnyAsync(x => x.Id == request.ProjectId, ct);

        if (!projectExists)
            throw new("Project not found");

        return await db.Projects
            .AsNoTracking()
            .Where(x => x.Id == request.ProjectId)
            .SelectMany(x => x.WorkTasks)
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