using MediatR;

namespace ProjectManagement.Api.Features.Users.Delete;

public record DeleteUserCommand(Guid UserId)
    : IRequest;