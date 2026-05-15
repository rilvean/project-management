using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectManagement.Domain.Models;
using ProjectManagement.Infrastructure.Persistence.Extensions;

namespace ProjectManagement.Infrastructure.Persistence.Configurations;

public class ProjectConfiguration : IEntityTypeConfiguration<Project>
{
    public void Configure(EntityTypeBuilder<Project> builder)
    {
        builder.ToTable("projects");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.ManagerId)
            .IsRequired(false);

        builder.Property(x => x.Title)
            .HasMaxLength(Project.MaxTitleLength)
            .IsRequired();

        builder.Property(x => x.Description)
            .HasMaxLength(Project.MaxDescriptionLength)
            .IsRequired(false);

        builder.Property(x => x.Priority)
            .IsRequired();

        builder.Property(x => x.Status)
            .IsRequired();

        builder.Navigation(x => x.WorkTasks)
            .HasField("_workTasks")
            .UsePropertyAccessMode(PropertyAccessMode.Field);

        builder.Navigation(x => x.Executors)
            .HasField("_executors")
            .UsePropertyAccessMode(PropertyAccessMode.Field);

        builder.HasIndex(x => new { x.ManagerId, x.Title })
            .IsUnique();

        builder.AddAudit();
    }
}