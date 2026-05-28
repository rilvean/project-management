namespace ProjectManagement.Api.Features.Users.Shared;

public sealed record UserResponse(
    Guid Id,
    string Name,
    string Email,
    string Role
);