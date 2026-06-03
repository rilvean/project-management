using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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


        services.AddDbContext<WriteDbContext>(options =>
            options.ConfigureNpgsql(connectionString)
        );

        services.AddDbContext<ReadDbContext>(options =>
            options.ConfigureNpgsql(connectionString)
        );

        return services;
    }
}