using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ProjectManagement.Infrastructure.Persistence.Extensions;

public static class AuditConfigurationExtensions
{
    public static void AddAudit<TEntity>(this EntityTypeBuilder<TEntity> builder)
        where TEntity : class
    {
        builder.Property<DateTime>("CreatedAt")
            .HasColumnType("timestamptz")
            .HasPrecision(0)
            .IsRequired();

        builder.Property<DateTime>("UpdatedAt")
            .HasColumnType("timestamptz")
            .HasPrecision(0)
            .IsRequired();
    }
}