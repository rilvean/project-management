using MediatR;
using ProjectManagement.Api.Features.Shared;

namespace ProjectManagement.Api.Features.Users.GetPage;

public sealed record GetUsersPageQuery(
    int Page = 1,
    int PageSize = 20
) : IRequest<PagedResponse<UserResponse>>;