using Microsoft.EntityFrameworkCore;
using ProjectManagement.Domain.Enums;
using ProjectManagement.Infrastructure.Persistence.Interceptors;

namespace ProjectManagement.Infrastructure.Persistence.Extensions;

static class DbContextOptionBuilderExtensions
{
    public static DbContextOptionsBuilder ConfigureNpgsql(
        this DbContextOptionsBuilder options,
        string connectionString,
        AuditInterceptor? interceptor = null)
    {
        options.UseNpgsql(
                connectionString,
                o =>
                {
                    o.MapEnum<UserRole>();
                    o.MapEnum<WorkTaskStatus>();
                    o.MapEnum<ProjectPriority>();
                    o.MapEnum<ProjectStatus>();
                })
            .UseSnakeCaseNamingConvention()
            .AddInterceptors(interceptor ?? new AuditInterceptor());

        return options;
    }

    public static DbContextOptionsBuilder<T> ConfigureNpgsql<T>(
        this DbContextOptionsBuilder<T> options,
        string connectionString,
        AuditInterceptor? interceptor = null)
        where T : DbContext
    {
        if (options is DbContextOptionsBuilder o)
            o.ConfigureNpgsql(connectionString, interceptor);

        return options;
    }
}