using MediatR;
using ProjectManagement.Infrastructure.Persistence;

namespace ProjectManagement.Api.Features.Users.ChangeRole;

public class Handler(ProjectManagementDbContext db)
    : IRequestHandler<ChangeRoleCommand>
{
    public async Task Handle(ChangeRoleCommand request, CancellationToken ct)
    {
        var user = await db.Users.FindAsync(request.UserId, ct);

        if (user is null)
            throw new("User not found");

        user.ChangeRole(request.Role);

        await db.SaveChangesAsync(ct);
    }
}