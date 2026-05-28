using MediatR;

namespace ProjectManagement.Api.Features.Auth.Login;

public sealed record LoginCommand(
    string Email,
    string Password
) : IRequest<LoginResponse>;