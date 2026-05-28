using MediatR;
using ProjectManagement.Domain.ValueObjects;
using ProjectManagement.Infrastructure.Persistence;

namespace ProjectManagement.Api.Features.Users.ChangeEmail;

public class Handler(ProjectManagementDbContext db)
    : IRequestHandler<ChangeEmailCommand>
{
    public async Task Handle(ChangeEmailCommand request, CancellationToken ct)
    {
        var user = await db.Users.FindAsync(request.UserId, ct);

        if (user is null)
            throw new("User not found");

        user.ChangeEmail(Email.Create(request.Email));

        await db.SaveChangesAsync(ct);
    }
}