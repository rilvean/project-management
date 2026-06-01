using MediatR;
using ProjectManagement.Api.Features.Shared;
using ProjectManagement.Api.Shared;

namespace ProjectManagement.Api.Features.Users.GetById;

public sealed record GetUserByIdQuery(Guid UserId)
    : IRequest<UserResponse>;