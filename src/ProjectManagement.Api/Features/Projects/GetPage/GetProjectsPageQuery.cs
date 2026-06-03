using MediatR;
using ProjectManagement.Api.Features.Projects.Shared;
using ProjectManagement.Api.Features.Shared;

namespace ProjectManagement.Api.Features.Projects.GetPage;

public sealed record GetProjectsPageQuery(
    int Page = 1,
    int PageSize = 20
) : IRequest<PagedResponse<ProjectResponse>>;