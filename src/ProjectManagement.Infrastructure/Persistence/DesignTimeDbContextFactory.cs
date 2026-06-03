using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using ProjectManagement.Infrastructure.Persistence.Extensions;

namespace ProjectManagement.Infrastructure.Persistence;

public sealed class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<WriteDbContext>
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