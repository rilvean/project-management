using MediatR;
using Microsoft.EntityFrameworkCore;
using ProjectManagement.Api.Features.Shared;
using ProjectManagement.Infrastructure.Persistence;

namespace ProjectManagement.Api.Features.WorkTasks.GetMy;

public sealed class Handler(ReadDbContext db)
    : IRequestHandler<GetMyWorkTasksQuery, List<WorkTaskResponse>>
{
    public Task<List<WorkTaskResponse>> Handle(GetMyWorkTasksQuery request, CancellationToken ct)
    {
        return db.WorkTasks
            .Where(x => x.ExecutorId == request.EmployeeId)
            .OrderBy(x => x.Id)
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