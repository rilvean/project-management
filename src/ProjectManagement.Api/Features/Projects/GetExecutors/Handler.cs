using MediatR;
using Microsoft.EntityFrameworkCore;
using ProjectManagement.Api.Features.Shared;
using ProjectManagement.Infrastructure.Persistence;

namespace ProjectManagement.Api.Features.Projects.GetExecutors;

public class Handler(ProjectManagementDbContext db)
    : IRequestHandler<GetExecutorsQuery, List<UserResponse>>
{
    public async Task<List<UserResponse>> Handle(GetExecutorsQuery request, CancellationToken ct)
    {
        var projectExists = await db.Projects
            .AnyAsync(x => x.Id == request.ProjectId, ct);

        if (!projectExists)
            throw new("Project not found");

        return await db.Projects
            .AsNoTracking()
            .Where(x => x.Id == request.ProjectId)
            .SelectMany(x => x.Executors)
            .Join(
                db.Users,
                executor => executor.UserId,
                user => user.Id,
                (executor, user) => new UserResponse(
                    user.Id,
                    user.Name,
                    user.Email,
                    user.Role
                )
            )
            .ToListAsync(ct);
    }
}