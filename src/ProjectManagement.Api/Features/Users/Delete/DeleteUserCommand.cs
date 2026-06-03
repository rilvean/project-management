using MediatR;

namespace ProjectManagement.Api.Features.Users.Delete;

public sealed record DeleteUserCommand(Guid UserId)
    : IRequest;