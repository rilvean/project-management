using ProjectManagement.Domain.Enums;

namespace ProjectManagement.Api.Features.Shared;

public sealed record UserResponse(
    Guid Id,
    string Name,
    string Email,
    UserRole Role
);