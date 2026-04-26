using BookLibraryApi.src.Domain.Models;
using BookLibraryApi.src.Application.Abstractions;
using BookLibraryApi.src.Domain.DTOs;
using BookLibraryApi.src.Infrastructures.DataAccess.Data;
using Microsoft.EntityFrameworkCore;

namespace BookLibraryApi.src.Application.Services;

public class BooksService : IBooksService
{
    private readonly AppDbContext _db;

    public BooksService(AppDbContext db)
    {
        _db = db;
    }

    public async Task<List<Book>> GetAll(CancellationToken ct)
    {
        return await _db.Books.ToListAsync(ct);
    }


    public async Task<Book?> GetById(Guid id, CancellationToken ct)
    {
        var book = await _db.Books.FindAsync([id], ct);

        if (book is null)
        {
            return null;
        }

        return book;
    }

    public async Task<Book> Create(CreateBookRequest request, CancellationToken ct)
    {
        var book = new Book
        {
            Title = request.Title,
            Author = request.Author,
            Genre = request.Genre,
            Price = request.Price,
            Year = request.Year
        };

        _db.Books.Add(book);
        await _db.SaveChangesAsync(ct);

        return book;
    }

    public async Task Delete(Guid id, CancellationToken ct)
    {
        var book = await _db.Books.FindAsync([id], ct);

        if (book is null)
        {
            return;
        }

        _db.Books.Remove(book);
        await _db.SaveChangesAsync(ct);
    }

    public async Task<Book?> Update(Guid id, UpdateBookRequest request, CancellationToken ct)
    {
        var oldBook = await _db.Books.FindAsync([id], ct);

        if (oldBook is null)
        {
            return null;
        }

        oldBook.Title = request.Title;
        oldBook.Author = request.Author;
        oldBook.Genre = request.Genre;
        oldBook.Price = request.Price;
        oldBook.Year = request.Year;

        await _db.SaveChangesAsync(ct);
        return oldBook;
    }
}
