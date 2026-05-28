using MediatR;

namespace ProjectManagement.Api.Features.Users.ChangeEmail;

public record ChangeEmailCommand(
    Guid UserId,
    string Email
) : IRequest;