using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProjectManagement.Domain.Enums;
using ProjectManagement.Infrastructure.Persistence.Interceptors;

namespace ProjectManagement.Infrastructure.Persistence.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString =
            configuration.GetConnectionString("Default")
            ?? throw new InvalidOperationException("Connection string not found.");

        services.AddScoped<AuditInterceptor>();

        services.AddDbContext<ProjectManagementDbContext>((sp, options) =>
        {
            options
                .UseNpgsql(
                    connectionString,
                    o =>
                    {
                        o.MapEnum<UserRole>();
                        o.MapEnum<WorkTaskStatus>();
                        o.MapEnum<ProjectPriority>();
                        o.MapEnum<ProjectStatus>();
                    })
                .UseSnakeCaseNamingConvention()
                .AddInterceptors(sp.GetRequiredService<AuditInterceptor>());
        });

        return services;
    }
}