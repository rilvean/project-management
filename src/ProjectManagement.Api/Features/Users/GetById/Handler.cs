using MediatR;
using ProjectManagement.Api.Features.Users.Shared;
using ProjectManagement.Infrastructure.Persistence;

namespace ProjectManagement.Api.Features.Users.GetById;

public sealed class Handler(ProjectManagementDbContext db)
    : IRequestHandler<GetUserByIdQuery, UserResponse>
{
    public async Task<UserResponse> Handle(GetUserByIdQuery request, CancellationToken ct)
    {
        var user = await db.Users.FindAsync(request.UserId, ct);

        if (user is null)
            throw new("User not found");

        return new UserResponse(
            user.Id,
            user.Name,
            user.Email.Value,
            user.Role.ToString()
        );
    }
}