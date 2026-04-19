using BookLibraryApi.Data;
using BookLibraryApi.DTOs;
using BookLibraryApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BookLibraryApi.Services;

public class BooksService
{
    private readonly AppDbContext _db;

    public BooksService(AppDbContext db)
    {
        _db = db;
    }

    public async Task<List<BookResponse>> GetAll(CancellationToken ct)
    {
        return await _db.Books
            .Select(book => new BookResponse
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author,
                Price = book.Price
            })
            .ToListAsync(ct);
    }


    public async Task<BookResponse?> GetById(Guid id, CancellationToken ct)
    {
        var book = await _db.Books.FindAsync([id], ct);

        if (book is null)
        {
            return null;
        }

        return new BookResponse
        {
            Id = book.Id,
            Title = book.Title,
            Author = book.Author,
            Price = book.Price
        };
    }

    public async Task<BookResponse> Create(CreateBookRequest request, CancellationToken ct)
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

        return new BookResponse
        {
            Id = book.Id,
            Title = book.Title,
            Author = book.Author,
            Price = book.Price
        };
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

    public async Task<BookResponse?> Update(Guid id, CreateBookRequest request, CancellationToken ct)
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
        return new BookResponse
        {
            Id = oldBook.Id,
            Title = oldBook.Title,
            Author = oldBook.Author,
            Price = oldBook.Price
        };
    }
}
