using BookLibraryApi.src.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookLibraryApi.src.Infrastructures.DataAccess.Data.Configurations;

public class BookEntityConfiguration : IEntityTypeConfiguration<Book>
{
    public virtual void Configure(EntityTypeBuilder<Book> builder)
    {
        builder.HasKey(b => b.Id);
        builder.Property(b => b.Id).ValueGeneratedOnAdd();

        builder.Property(b => b.Title)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(b => b.Author)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(b => b.Genre)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(b => b.Price)
            .IsRequired();

        builder.Property(b => b.Year)
            .IsRequired();

        builder.HasMany(b => b.Reviews)
            .WithOne(r => r.Book)
            .HasForeignKey(r => r.BookId);
    }
}
