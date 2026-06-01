using MediatR;
using Microsoft.EntityFrameworkCore;
using ProjectManagement.Api.Features.Projects.GetById;
using ProjectManagement.Api.Features.Projects.Shared;
using ProjectManagement.Api.Features.Shared;
using ProjectManagement.Infrastructure.Persistence;

namespace ProjectManagement.Api.Features.WorkTasks.GetById;

public class Handler(ProjectManagementDbContext db)
    : IRequestHandler<GetWorkTaskByIdQuery, WorkTaskResponse>
{
    public async Task<WorkTaskResponse> Handle(GetWorkTaskByIdQuery request, CancellationToken ct)
    {
        var workTask = await db.Projects
            .AsNoTracking()
            .Where(p => p.Id == request.WorkTaskId)
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
            .FirstOrDefaultAsync(ct);

        if (workTask is null)
            throw new("WorkTask not found");

        return workTask;
    }
}