using BookLibraryApi.src.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookLibraryApi.src.Infrastructures.DataAccess.Data.Configurations;

internal class ReviewEntityConfiguration : IEntityTypeConfiguration<Review>
{
    public virtual void Configure(EntityTypeBuilder<Review> builder)
    {
        builder.HasKey(r => r.Id);
        builder.Property(r => r.Id).ValueGeneratedOnAdd();

        builder.Property(r => r.Rating).IsRequired();

        builder.Property(r => r.Comment).IsRequired(false);

        builder.Property(r => r.CreatedAt).IsRequired();

        builder.HasOne(r => r.Book)
            .WithMany(b => b.Reviews)
            .HasForeignKey(r => r.BookId);

        builder.HasOne(r => r.User)
            .WithMany(u => u.Reviews)
            .HasForeignKey(r => r.UserId);




    }
}
