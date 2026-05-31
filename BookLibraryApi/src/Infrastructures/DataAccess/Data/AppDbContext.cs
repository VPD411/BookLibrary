using BookLibraryApi.src.Domain.Models;
using BookLibraryApi.src.Infrastructures.DataAccess.Data.Configurations;
using Microsoft.EntityFrameworkCore;

namespace BookLibraryApi.src.Infrastructures.DataAccess.Data;

public class AppDbContext : DbContext
{
    public DbSet<Book> Books => Set<Book>();
    public DbSet<User> Users => Set<User>();
    public DbSet<Review> Reviews => Set<Review>();

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        CustomModelBuilder.OnModelCreating(builder);

        base.OnModelCreating(builder);
    }
}
