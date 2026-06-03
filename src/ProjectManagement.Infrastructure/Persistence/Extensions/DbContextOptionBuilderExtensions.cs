using Microsoft.EntityFrameworkCore;
using ProjectManagement.Domain.Enums;

namespace ProjectManagement.Infrastructure.Persistence.Extensions;

static class DbContextOptionBuilderExtensions
{
    public static DbContextOptionsBuilder ConfigureNpgsql(
        this DbContextOptionsBuilder options,
        string connectionString)
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
            .UseSnakeCaseNamingConvention();

        return options;
    }

    public static DbContextOptionsBuilder<T> ConfigureNpgsql<T>(
        this DbContextOptionsBuilder<T> options,
        string connectionString)
        where T : DbContext
    {
        if (options is DbContextOptionsBuilder o)
            o.ConfigureNpgsql(connectionString);

        return options;
    }
}