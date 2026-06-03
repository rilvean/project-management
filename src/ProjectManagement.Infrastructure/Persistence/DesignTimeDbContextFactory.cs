using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using ProjectManagement.Domain.Enums;
using ProjectManagement.Infrastructure.Persistence.Extensions;
using ProjectManagement.Infrastructure.Persistence.Interceptors;

namespace ProjectManagement.Infrastructure.Persistence;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<WriteDbContext>
{
    public WriteDbContext CreateDbContext(string[] args)
    {
        var connectionString =
            "Host=localhost;"
            + "Database=project_management;"
            + "Username=postgres;Password=postgres;";

        var options = new DbContextOptionsBuilder<WriteDbContext>()
            .ConfigureNpgsql(connectionString)
            .Options;

        return new WriteDbContext(options);
    }
}