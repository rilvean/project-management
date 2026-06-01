using MediatR;
using ProjectManagement.Api.Features.Shared;
using ProjectManagement.Api.Shared;

namespace ProjectManagement.Api.Features.Users.GetPage;

public record GetUsersPageQuery(
    int Page = 1,
    int PageSize = 20
) : IRequest<PagedResponse<UserResponse>>;