using BookLibraryApi.src.Domain.Models;

namespace BookLibraryApi.src.Application.Abstractions.DataAccess.Repositories
{
    public interface IBooksRepository
    {
        Task<Book> CreateAsync(Book book, CancellationToken ct);
        void Delete(Book book);
        Task<IEnumerable<Book>> GetAllAsync(CancellationToken ct);
        Task<Book?> GetByIdAsync(Guid id, CancellationToken ct);
        Book Update(Book book);
    }
}