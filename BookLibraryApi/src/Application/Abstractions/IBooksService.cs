using BookLibraryApi.src.Domain.DTOs;
using BookLibraryApi.src.Domain.Models;

namespace BookLibraryApi.src.Application.Abstractions;

public interface IBooksService
{
    public Task<List<Book>> GetAll(CancellationToken ct);
    public Task<Book?> GetById(Guid id, CancellationToken ct);
    public Task<Book> Create(CreateBookRequest request, CancellationToken ct);
    public Task Delete(Guid id, CancellationToken ct);
    public Task<Book?> Update(Guid id, UpdateBookRequest request, CancellationToken ct);
}