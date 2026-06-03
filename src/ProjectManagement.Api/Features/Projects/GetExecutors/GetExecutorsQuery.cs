using MediatR;
using ProjectManagement.Api.Features.Shared;

namespace ProjectManagement.Api.Features.Projects.GetExecutors;

public sealed record GetExecutorsQuery(Guid ProjectId)
    : IRequest<List<UserResponse>>;