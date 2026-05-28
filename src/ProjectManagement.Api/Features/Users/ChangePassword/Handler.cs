using MediatR;
using ProjectManagement.Infrastructure.Persistence;

namespace ProjectManagement.Api.Features.Users.ChangePassword;

public class Handler(ProjectManagementDbContext db)
    : IRequestHandler<ChangePasswordCommand>
{
    public async Task Handle(ChangePasswordCommand request, CancellationToken ct)
    {
        var user = await db.Users.FindAsync(request.UserId, ct);
        
        if (user is null)
            throw new("User not found");
        
        user.ChangePassword(BCrypt.Net.BCrypt.HashPassword(request.Password));
        
        await db.SaveChangesAsync(ct);
    }
}