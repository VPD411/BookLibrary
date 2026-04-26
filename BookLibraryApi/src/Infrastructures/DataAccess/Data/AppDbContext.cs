using BookLibraryApi.src.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace BookLibraryApi.src.Infrastructures.DataAccess.Data;

public class AppDbContext : DbContext
{
    public DbSet<Book> Books => Set<Book>();

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }
}
