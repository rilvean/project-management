using MediatR;
using Microsoft.EntityFrameworkCore;
using ProjectManagement.Infrastructure.Persistence;

namespace ProjectManagement.Api.Features.Users.Delete;

public sealed class Handler(WriteDbContext db)
    : IRequestHandler<DeleteUserCommand>
{
    public async Task Handle(DeleteUserCommand request, CancellationToken ct)
    {
        await db.Users
            .Where(x => x.Id == request.UserId)
            .ExecuteDeleteAsync(ct);
    }
}