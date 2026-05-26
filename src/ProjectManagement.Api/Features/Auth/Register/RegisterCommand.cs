using MediatR;
using ProjectManagement.Domain.Enums;

namespace ProjectManagement.Api.Features.Auth.Register;

public sealed record RegisterCommand(
    string Name,
    string Email,
    string Password,
    UserRole Role
) : IRequest<Guid>;