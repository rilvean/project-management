using MediatR;
using Microsoft.EntityFrameworkCore;
using ProjectManagement.Api.Features.Shared;
using ProjectManagement.Infrastructure.Persistence;

namespace ProjectManagement.Api.Features.WorkTasks.GetById;

public sealed class Handler(ReadDbContext db)
    : IRequestHandler<GetWorkTaskByIdQuery, WorkTaskResponse>
{
    public async Task<WorkTaskResponse> Handle(GetWorkTaskByIdQuery request, CancellationToken ct)
    {
        var workTask = await db.WorkTasks
            .Where(x => x.Id == request.WorkTaskId)
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