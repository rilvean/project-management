using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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

        services.AddDbContext<WriteDbContext>((sp, options) =>
            options.ConfigureNpgsql(connectionString,
                sp.GetRequiredService<AuditInterceptor>())
        );

        services.AddDbContext<ReadDbContext>(options =>
            options.ConfigureNpgsql(connectionString)
        );

        return services;
    }
}