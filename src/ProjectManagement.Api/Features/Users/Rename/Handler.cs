using MediatR;
using ProjectManagement.Infrastructure.Persistence;

namespace ProjectManagement.Api.Features.Users.Rename;

public sealed class Handler(ProjectManagementDbContext db)
    : IRequestHandler<RenameUserCommand>
{
    public async Task Handle(RenameUserCommand request, CancellationToken ct)
    {
        var user = await db.Users.FindAsync(request.UserId, ct);

        if (user is null)
            throw new("User not found");

        user.Rename(request.Name);

        await db.SaveChangesAsync(ct);
    }
}