using MediatR;
using Microsoft.EntityFrameworkCore;
using ProjectManagement.Api.Features.Shared;
using ProjectManagement.Infrastructure.Persistence;

namespace ProjectManagement.Api.Features.Users.GetPage;

public sealed class Handler(ReadDbContext db)
    : IRequestHandler<GetUsersPageQuery, PagedResponse<UserResponse>>
{
    public async Task<PagedResponse<UserResponse>> Handle(GetUsersPageQuery request, CancellationToken ct)
    {
        var query = db.Users;

        var total = query.CountAsync(ct);

        var items = query
            .OrderBy(x => x.Id)
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .Select(x => new UserResponse(
                x.Id,
                x.Name,
                x.Email,
                x.Role
            ))
            .ToListAsync(ct);

        return new PagedResponse<UserResponse>(
            await items,
            await total,
            request.Page,
            request.PageSize
        );
    }
}