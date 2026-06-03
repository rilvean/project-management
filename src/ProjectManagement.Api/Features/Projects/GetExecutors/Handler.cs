using MediatR;
using Microsoft.EntityFrameworkCore;
using ProjectManagement.Api.Features.Shared;
using ProjectManagement.Infrastructure.Persistence;

namespace ProjectManagement.Api.Features.Projects.GetExecutors;

public class Handler(ReadDbContext db)
    : IRequestHandler<GetExecutorsQuery, List<UserResponse>>
{
    public async Task<List<UserResponse>> Handle(GetExecutorsQuery request, CancellationToken ct)
    {
        var projectExists = await db.Projects
            .AnyAsync(x => x.Id == request.ProjectId, ct);

        if (!projectExists)
            throw new("Project not found");

        return await db.ProjectExecutors
            .Where(x => x.ProjectId == request.ProjectId)
            .Join(
                db.Users,
                e => e.UserId,
                u => u.Id,
                (e, u) => new UserResponse(
                    u.Id,
                    u.Name,
                    u.Email,
                    u.Role
                )
            )
            .ToListAsync(ct);
    }
}