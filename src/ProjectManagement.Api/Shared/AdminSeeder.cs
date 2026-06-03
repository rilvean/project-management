using Microsoft.EntityFrameworkCore;
using ProjectManagement.Domain.Enums;
using ProjectManagement.Domain.Models;
using ProjectManagement.Domain.ValueObjects;
using ProjectManagement.Infrastructure.Persistence;
using ProjectManagement.Infrastructure.Persistence.Shared;

namespace ProjectManagement.Api.Shared;

public sealed class AdminSeeder(WriteDbContext context) : IDataSeeder
{
    public async Task SeedAsync()
    {
        if (await context.Users.AnyAsync(u => u.Role == UserRole.Admin))
            return;

        var admin = new User(
            name: "Admin",
            email: Email.Create("admin@example.com"),
            passwordHash: BCrypt.Net.BCrypt.HashPassword("admin123"),
            role: UserRole.Admin
        );

        context.Users.Add(admin);
        await context.SaveChangesAsync();
    }
}