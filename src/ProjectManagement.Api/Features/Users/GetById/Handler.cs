using MediatR;
using Microsoft.EntityFrameworkCore;
using ProjectManagement.Api.Features.Shared;
using ProjectManagement.Api.Shared;
using ProjectManagement.Infrastructure.Persistence;

namespace ProjectManagement.Api.Features.Users.GetById;

public sealed class Handler(ProjectManagementDbContext db)
    : IRequestHandler<GetUserByIdQuery, UserResponse>
{
    public async Task<UserResponse> Handle(GetUserByIdQuery request, CancellationToken ct)
    {
        var user = await db.Users
            .AsNoTracking()
            .Where(x => x.Id == request.UserId)
            .Select(x => new UserResponse(
                x.Id,
                x.Name,
                x.Email.Value,
                x.Role
            ))
            .FirstOrDefaultAsync(ct);

        if (user is null)
            throw new("User not found");

        return user;
    }
}