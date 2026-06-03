using MediatR;

namespace ProjectManagement.Api.Features.Users.ChangePassword;

public sealed record ChangePasswordCommand(
    Guid UserId,
    string Password
) : IRequest;