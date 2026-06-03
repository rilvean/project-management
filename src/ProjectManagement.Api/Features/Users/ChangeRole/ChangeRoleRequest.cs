using ProjectManagement.Domain.Enums;

namespace ProjectManagement.Api.Features.Users.ChangeRole;

public sealed record ChangeRoleRequest(UserRole Role);