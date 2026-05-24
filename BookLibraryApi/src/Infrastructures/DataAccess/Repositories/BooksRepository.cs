using BookLibraryApi.src.Application.Abstractions.DataAccess.Repositories;
using BookLibraryApi.src.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace BookLibraryApi.src.Infrastructures.DataAccess.Repositories;

public class BooksRepository : IBooksRepository
{
    private readonly DbSet<Book> _db;

    public BooksRepository(DbSet<Book> db)
    {
        _db = db;
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
