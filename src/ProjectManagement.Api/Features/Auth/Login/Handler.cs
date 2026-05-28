using MediatR;
using Microsoft.EntityFrameworkCore;
using ProjectManagement.Api.Services;
using ProjectManagement.Infrastructure.Persistence;

namespace ProjectManagement.Api.Features.Auth.Login;

public sealed class Handler(
    ProjectManagementDbContext db,
    JwtProvider jwtProvider)
    : IRequestHandler<LoginCommand, LoginResponse>
{
    public async Task<LoginResponse> Handle(
        LoginCommand request,
        CancellationToken ct)
    {
        var user = await db.Users
            .FirstOrDefaultAsync(x => x.Email == request.Email, ct);

        if (user is null)
            throw new("Invalid credentials");

        var isValid = BCrypt.Net.BCrypt.Verify(
            request.Password,
            user.PasswordHash);

        if (!isValid)
            throw new("Invalid credentials");

        var token = jwtProvider.Generate(user);

        return new LoginResponse(token);
    }
}