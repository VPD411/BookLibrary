using BookLibraryApi.src.Domain.Models;
using BookLibraryApi.src.Infrastructures.DataAccess.Data;
using Microsoft.EntityFrameworkCore;

namespace BookLibraryApi.src.Infrastructures.DataAccess.Repositories;

public class ReviewsRepository
{
    private readonly AppDbContext _context;
    private readonly DbSet<Review> _db;

    public ReviewsRepository(AppDbContext context)
    {
        _context = context;
        _db = context.Set<Review>();
    }

    public async Task<IEnumerable<Review>> GetAllAsync(CancellationToken ct)
    {
        var result = await _db.ToListAsync(ct);
        return result;
    }

    public async Task<Review?> GetByIdAsync(Guid id, CancellationToken ct)
    {
        var result = await _db.FindAsync([id], ct);
        return result;
    }

    public async Task<Review> CreateAsync(Review review, CancellationToken ct)
    {
        var result = await _db.AddAsync(review, ct);
        return result.Entity;
    }

    public Review Update(Review review)
    {
        var result = _db.Update(review);
        return result.Entity;
    }

    public void Delete(Review review)
    {
        _db.Remove(review);
    }
}
