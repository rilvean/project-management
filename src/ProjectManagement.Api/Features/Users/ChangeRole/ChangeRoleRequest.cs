using ProjectManagement.Domain.Enums;

namespace ProjectManagement.Api.Features.Users.ChangeRole;

public record ChangeRoleRequest(UserRole Role);