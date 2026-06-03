using MediatR;

namespace ProjectManagement.Api.Features.Users.Rename;

public sealed record RenameUserCommand(
    Guid UserId,
    string Name
) : IRequest;