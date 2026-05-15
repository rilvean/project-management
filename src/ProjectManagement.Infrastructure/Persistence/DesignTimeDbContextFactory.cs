using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using ProjectManagement.Domain.Enums;
using ProjectManagement.Infrastructure.Persistence.Interceptors;

namespace ProjectManagement.Infrastructure.Persistence;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ProjectManagementDbContext>
{
    public ProjectManagementDbContext CreateDbContext(string[] args)
    {
        var connectionString =
            "Host=localhost;"
            + "Database=project_management;"
            + "Username=postgres;Password=postgres;";

        var options =
            new DbContextOptionsBuilder<ProjectManagementDbContext>()
                .UseNpgsql(
                    connectionString,
                    o =>
                    {
                        o.MapEnum<UserRole>();
                        o.MapEnum<WorkTaskStatus>();
                        o.MapEnum<ProjectPriority>();
                        o.MapEnum<ProjectStatus>();
                    }
                )
                .UseSnakeCaseNamingConvention()
                .AddInterceptors(new AuditInterceptor())
                .Options;

        return new ProjectManagementDbContext(options);
    }
}