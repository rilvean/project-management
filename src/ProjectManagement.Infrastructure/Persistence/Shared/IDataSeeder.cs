namespace ProjectManagement.Infrastructure.Persistence.Shared;

public interface IDataSeeder
{
    Task SeedAsync();
}