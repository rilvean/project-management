using Microsoft.EntityFrameworkCore;
using ProjectManagement.Domain.Enums;
using ProjectManagement.Domain.Models;

namespace ProjectManagement.Infrastructure.Persistence;

public class ProjectManagementDbContext(DbContextOptions<ProjectManagementDbContext> options)
    : DbContext(options)
{
    public DbSet<User> Users => Set<User>();
    public DbSet<Project> Projects => Set<Project>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.HasPostgresEnum<UserRole>();
        modelBuilder.HasPostgresEnum<WorkTaskStatus>();
        modelBuilder.HasPostgresEnum<ProjectPriority>();
        modelBuilder.HasPostgresEnum<ProjectStatus>();

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProjectManagementDbContext).Assembly);
    }
}