using MediatR;

namespace ProjectManagement.Api.Features.Users.ChangeEmail;

public sealed record ChangeEmailCommand(
    Guid UserId,
    string Email
) : IRequest;