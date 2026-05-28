using MediatR;
using ProjectManagement.Domain.Enums;

namespace ProjectManagement.Api.Features.Users.ChangeRole;

public record ChangeRoleCommand(
    Guid UserId,
    UserRole Role
) : IRequest;