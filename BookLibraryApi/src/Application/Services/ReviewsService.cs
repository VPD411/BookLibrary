using BookLibraryApi.src.Application.Abstractions.DataAccess;
using BookLibraryApi.src.Application.Abstractions.DataAccess.Repositories;
using BookLibraryApi.src.Application.Abstractions.Services;
using BookLibraryApi.src.Domain.DTOs;
using BookLibraryApi.src.Domain.Models;

namespace BookLibraryApi.src.Application.Services;

public class ReviewsService : IReviewsService
{
    private readonly IReviewsRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IBooksRepository _booksRepository;
    private readonly IUsersRepository _usersRepository;

    public ReviewsService(IReviewsRepository repository, IUnitOfWork unitOfWork, IBooksRepository booksRepository, IUsersRepository usersRepository)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _booksRepository = booksRepository;
        _usersRepository = usersRepository;
    }

    public async Task<IEnumerable<Review>> GetAllAsync(CancellationToken ct)
    {
        var result = await _repository.GetAllAsync(ct);
        return result;
    }

    public async Task<Review?> GetByIdAsync(Guid id, CancellationToken ct)
    {
        var result = await _repository.GetByIdAsync(id, ct);
        return result;
    }

    public async Task<Review> CreateAsync(CreateReviewRequest request, CancellationToken ct)
    {
        var book = await _booksRepository.GetByIdAsync(request.BookId, ct);
        if (book is null)
        {
            throw new ArgumentException("Book not found");
        }

        var user = await _usersRepository.GetByIdAsync(request.UserId, ct);
        if (user is null)
        {
            throw new ArgumentException("User not found");
        }

        if (request.Rating < 1 || request.Rating > 5)
        {
            throw new ArgumentException("Rating value is invalid");
        }

        var review = new Review
        {
            Rating = request.Rating,
            Comment = request.Comment,
            BookId = request.BookId,
            UserId = request.UserId,
        };

        var result = await _repository.CreateAsync(review, ct);
        await _unitOfWork.SaveChangesAsync(ct);
        return result;
    }

    public async Task<Review> UpdateAsync(Review review, CancellationToken ct)
    {
        var result = _repository.Update(review);
        await _unitOfWork.SaveChangesAsync(ct);
        return result;
    }

    public async Task DeleteAsync(Guid id, CancellationToken ct)
    {
        var review = await _repository.GetByIdAsync(id, ct);
        if (review is not null)
        {
            _repository.Delete(review);
            await _unitOfWork.SaveChangesAsync(ct);
        }
    }
}
