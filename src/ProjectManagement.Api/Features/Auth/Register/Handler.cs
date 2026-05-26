using MediatR;
using Microsoft.EntityFrameworkCore;
using ProjectManagement.Domain.Exceptions;
using ProjectManagement.Domain.Models;
using ProjectManagement.Domain.ValueObjects;
using ProjectManagement.Infrastructure.Persistence;

namespace ProjectManagement.Api.Features.Auth.Register;

public sealed class Handler(ProjectManagementDbContext db)
    : IRequestHandler<RegisterCommand, Guid>
{
    public async Task<Guid> Handle(
        RegisterCommand request,
        CancellationToken ct)
    {
        var exists = await db.Users
            .AnyAsync(x => x.Email == request.Email, ct);

        if (exists)
            throw new DomainRuleException("User already exists.");

        var passwordHash =
            BCrypt.Net.BCrypt.HashPassword(request.Password);

        var user = new User(
            request.Name,
            Email.Create(request.Email),
            passwordHash,
            request.Role);

        db.Users.Add(user);

        await db.SaveChangesAsync(ct);

        return user.Id;
    }
}