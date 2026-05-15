using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectManagement.Domain.Models;
using ProjectManagement.Infrastructure.Persistence.Extensions;

namespace ProjectManagement.Infrastructure.Persistence.Configurations;

public class WorkTaskConfiguration : IEntityTypeConfiguration<WorkTask>
{
    public void Configure(EntityTypeBuilder<WorkTask> builder)
    {
        builder.ToTable("work_tasks");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Title)
            .HasMaxLength(WorkTask.MaxTitleLength)
            .IsRequired();

        builder.Property(x => x.Description)
            .HasMaxLength(WorkTask.MaxDescriptionLength)
            .IsRequired(false);

        builder.Property(x => x.Deadline)
            .HasColumnType("timestamptz")
            .HasPrecision(0)
            .IsRequired(false);

        builder.Property(x => x.Status)
            .IsRequired();

        builder.HasOne<Project>()
            .WithMany(x => x.WorkTasks)
            .HasForeignKey(x => x.ProjectId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(x => x.ExecutorId)
            .OnDelete(DeleteBehavior.SetNull)
            .IsRequired(false);

        builder.HasIndex(x => new { x.ProjectId, x.Title })
            .IsUnique();

        builder.HasIndex(x => x.ExecutorId);

        builder.AddAudit();
    }
}