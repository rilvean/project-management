using MediatR;
using ProjectManagement.Api.Features.Users.Shared;

namespace ProjectManagement.Api.Features.Users.GetById;

public sealed record GetUserByIdQuery(Guid UserId)
    : IRequest<UserResponse>;