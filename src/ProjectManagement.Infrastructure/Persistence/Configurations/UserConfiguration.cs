using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectManagement.Domain.Models;
using ProjectManagement.Domain.ValueObjects;
using ProjectManagement.Infrastructure.Persistence.Extensions;

namespace ProjectManagement.Infrastructure.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .HasMaxLength(User.NameMaxLength)
            .IsRequired();

        builder.Property(x => x.Email)
            .HasConversion(
                e => e.Value,
                s => Email.Create(s)
            )
            .HasMaxLength(Email.MaxLenght)
            .IsRequired();

        builder.Property(x => x.PasswordHash)
            .IsRequired();

        builder.Property(x => x.Role)
            .IsRequired();

        builder.HasIndex(x => x.Email)
            .IsUnique();

        builder.AddAudit();
    }
}