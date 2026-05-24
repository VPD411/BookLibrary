using BookLibraryApi.src.Application.Abstractions.DataAccess.Repositories;
using BookLibraryApi.src.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BookLibraryApi.src.Infrastructures.DataAccess.Repositories;

public class UsersRepository : IUsersRepository
{
    private readonly DbSet<User> _db;

    public UsersRepository(DbSet<User> db)
    {
        _db = db;
    }

    public async Task<IEnumerable<User>> GetAllAsync(CancellationToken ct)
    {
        var result = await _db.ToListAsync(ct);
        return result;
    }

    public async Task<User?> GetWithPredicateAsync(Expression<Func<User, bool>> predicate, CancellationToken ct)
    {
        var result = await _db.FirstOrDefaultAsync(predicate, ct);
        return result;
    }

    public async Task<User?> GetByIdAsync(Guid id, CancellationToken ct)
    {
        var result = await _db.FindAsync([id], ct);
        return result;
    }

    public async Task<User> CreateAsync(User user, CancellationToken ct)
    {
        var result = await _db.AddAsync(user, ct);
        return result.Entity;
    }

    public User Update(User user)
    {
        var result = _db.Update(user);
        return result.Entity;
    }

    public void Delete(User user)
    {
        _db.Remove(user);
    }
}
