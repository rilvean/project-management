using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectManagement.Domain.Models;

namespace ProjectManagement.Infrastructure.Persistence.Configurations;

public class ProjectExecutorConfiguration : IEntityTypeConfiguration<ProjectExecutor>
{
    public void Configure(EntityTypeBuilder<ProjectExecutor> builder)
    {
        builder.ToTable("project_executors");

        builder.HasKey(x => new { x.ProjectId, x.UserId });

        builder.HasOne<Project>()
            .WithMany(x => x.Executors)
            .HasForeignKey(x => x.ProjectId);

        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(x => x.UserId);
    }
}