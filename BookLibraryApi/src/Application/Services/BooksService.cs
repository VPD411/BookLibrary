using BookLibraryApi.src.Domain.Models;
using BookLibraryApi.src.Domain.DTOs;
using BookLibraryApi.src.Application.Abstractions.Services;
using BookLibraryApi.src.Application.Abstractions.DataAccess.Repositories;

namespace BookLibraryApi.src.Application.Services;

public class BooksService : IBooksService
{
    private readonly IBooksRepository _repository;
    private readonly ILogger<BooksService> _logger;

    public BooksService(IBooksRepository repository, ILogger<BooksService> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<IEnumerable<Book>> GetAll(CancellationToken ct)
    {
        _logger.LogInformation("All books getting.");
        return await _repository.GetAllAsync(ct);
    }

    public async Task<Book?> GetById(Guid id, CancellationToken ct)
    {
        _logger.LogInformation("Searching book with ID {bookId}", id);
        return await _repository.GetByIdAsync(id, ct);
    }

    public async Task<Book> Create(CreateBookRequest request, CancellationToken ct)
    {
        _logger.LogInformation("Creating book");
        var book = new Book
        {
            Title = request.Title,
            Author = request.Author,
            Genre = request.Genre,
            Price = request.Price,
            Year = request.Year
        };

        var result = await _repository.CreateAsync(book, ct);
        return result;
    }

    public async Task Delete(Guid id, CancellationToken ct)
    {
        _logger.LogInformation("Deleting book with ID {id}", id);
        var book = await _repository.GetByIdAsync(id, ct);

        if (book is null)
        {
            return;
        }

        _repository.Delete(book);
    }

    public async Task<Book?> Update(Guid id, UpdateBookRequest request, CancellationToken ct)
    {
        _logger.LogInformation("Updating book with ID {id}", id);
        var oldBook = await _repository.GetByIdAsync(id, ct);

        if (oldBook is null)
        {
            return null;
        }

        oldBook.Title = request.Title;
        oldBook.Author = request.Author;
        oldBook.Genre = request.Genre;
        oldBook.Price = request.Price;
        oldBook.Year = request.Year;

        var newBook = _repository.Update(oldBook);
        return newBook;
    }
}
