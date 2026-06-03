using Microsoft.EntityFrameworkCore;
using ProjectManagement.Domain.Enums;

namespace ProjectManagement.Infrastructure.Persistence.Extensions;

static class ModelBuilderExtensions
{
    public static ModelBuilder AddEnums(this ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresEnum<UserRole>();
        modelBuilder.HasPostgresEnum<WorkTaskStatus>();
        modelBuilder.HasPostgresEnum<ProjectPriority>();
        modelBuilder.HasPostgresEnum<ProjectStatus>();

        return modelBuilder;
    }
}