using BookLibraryApi.src.Domain.DTOs;
using BookLibraryApi.src.Domain.Models;

namespace BookLibraryApi.src.Application.Abstractions.Services
{
    public interface IReviewsService
    {
        Task<Review> CreateAsync(CreateReviewRequest request, CancellationToken ct);
        Task DeleteAsync(Guid id, CancellationToken ct);
        Task<IEnumerable<Review>> GetAllAsync(CancellationToken ct);
        Task<Review?> GetByIdAsync(Guid id, CancellationToken ct);
        Task<Review> UpdateAsync(Review review, CancellationToken ct);
    }
}