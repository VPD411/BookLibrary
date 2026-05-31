using BookLibraryApi.src.Domain.Models;

namespace BookLibraryApi.src.Application.Abstractions.DataAccess.Repositories
{
    public interface IReviewsRepository
    {
        Task<Review> CreateAsync(Review review, CancellationToken ct);
        void Delete(Review review);
        Task<IEnumerable<Review>> GetAllAsync(CancellationToken ct);
        Task<Review?> GetByIdAsync(Guid id, CancellationToken ct);
        Review Update(Review review);
    }
}