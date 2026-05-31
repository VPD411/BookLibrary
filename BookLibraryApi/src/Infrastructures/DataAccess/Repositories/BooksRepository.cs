using BookLibraryApi.src.Application.Abstractions.DataAccess.Repositories;
using BookLibraryApi.src.Domain.Models;
using BookLibraryApi.src.Infrastructures.DataAccess.Data;
using Microsoft.EntityFrameworkCore;

namespace BookLibraryApi.src.Infrastructures.DataAccess.Repositories;

public class BooksRepository : IBooksRepository
{
    private readonly AppDbContext _context;
    private readonly DbSet<Book> _db;

    public BooksRepository(AppDbContext context)
    {
        _context = context;
        _db = context.Set<Book>();
    }

    public async Task<IEnumerable<Book>> GetAllAsync(CancellationToken ct)
    {
        var result = await _db.ToListAsync(ct);
        return result;
    }

    public async Task<Book?> GetByIdAsync(Guid id, CancellationToken ct)
    {
        var result = await _db.FindAsync([id], ct);
        return result;
    }

    public async Task<Book> CreateAsync(Book book, CancellationToken ct)
    {
        var result = await _db.AddAsync(book, ct);
        return result.Entity;
    }

    public Book Update(Book book)
    {
        var result = _db.Update(book);
        return result.Entity;
    }

    public void Delete(Book book)
    {
        _db.Remove(book);
    }
}
