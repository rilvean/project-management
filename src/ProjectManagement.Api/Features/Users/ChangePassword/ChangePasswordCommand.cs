using MediatR;

namespace ProjectManagement.Api.Features.Users.ChangePassword;

public record ChangePasswordCommand(
    Guid UserId,
    string Password
) : IRequest;