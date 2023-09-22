using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Models;

namespace Persistence.EntityTypeConfigurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(user => user.Id);
        builder.HasIndex(user => user.Id).IsUnique();
        builder.Property(user => user.Username).HasMaxLength(20);
        builder.HasIndex(user => user.Username).IsUnique();
        builder.Property(user => user.FirstName).HasMaxLength(20);
        builder.Property(user => user.LastName).HasMaxLength(20);
        builder.Property(user => user.Password).HasMaxLength(250);
    }
}