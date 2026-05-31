using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BookLibraryApi.src.Domain.Models;

namespace BookLibraryApi.src.Infrastructures.DataAccess.Data.Configurations;

internal class UserEntityConfiguration : IEntityTypeConfiguration<User>
{
    public virtual void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.Id);
        builder.Property(u => u.Id).ValueGeneratedOnAdd();

        builder.Property(u => u.Username)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(u => u.PasswordHash)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(u => u.Roles)
            .HasMaxLength(20)
            .IsRequired();

        builder.HasMany(u => u.Reviews)
            .WithOne(r => r.User)
            .HasForeignKey(u => u.UserId);
    }
}
