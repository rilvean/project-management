using MediatR;
using Microsoft.EntityFrameworkCore;
using ProjectManagement.Api.Features.Shared;
using ProjectManagement.Api.Features.Users.Shared;
using ProjectManagement.Infrastructure.Persistence;

namespace ProjectManagement.Api.Features.Users.GetPage;

public class Handler(ProjectManagementDbContext db)
    : IRequestHandler<GetUsersPageQuery, PagedResponse<UserResponse>>
{
    public async Task<PagedResponse<UserResponse>> Handle(
        GetUsersPageQuery request,
        CancellationToken ct)
    {
        var query = db.Users.AsNoTracking();

        var total = await query.CountAsync(ct);

        var items = await query
            .OrderBy(x => x.Id)
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .Select(x => new UserResponse(
                x.Id,
                x.Name,
                x.Email.Value,
                x.Role.ToString()
            ))
            .ToListAsync(ct);

        return new PagedResponse<UserResponse>(
            items,
            total,
            request.Page,
            request.PageSize
        );
    }
}