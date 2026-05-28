using MediatR;

namespace ProjectManagement.Api.Features.Users.Rename;

public record RenameUserCommand(
    Guid UserId,
    string Name
) : IRequest;