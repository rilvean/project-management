using MediatR;
using Microsoft.EntityFrameworkCore;
using ProjectManagement.Api.Features.Projects.Shared;
using ProjectManagement.Api.Features.Shared;
using ProjectManagement.Infrastructure.Persistence;

namespace ProjectManagement.Api.Features.Projects.GetPage;

public class Handler(ProjectManagementDbContext db)
    : IRequestHandler<GetProjectsPageQuery, PagedResponse<ProjectResponse>>
{
    public async Task<PagedResponse<ProjectResponse>> Handle(
        GetProjectsPageQuery request,
        CancellationToken ct)
    {
        var query = db.Projects.AsNoTracking();

        var total = await query.CountAsync(ct);

        var items = await query
            .OrderBy(x => x.Id)
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .Select(x => new ProjectResponse(
                x.Id,
                x.ManagerId,
                x.Title,
                x.Description,
                x.Priority,
                x.Status
            ))
            .ToListAsync(ct);

        return new PagedResponse<ProjectResponse>(
            items,
            total,
            request.Page,
            request.PageSize
        );
    }
}