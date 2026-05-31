using BookLibraryApi.src.Application.Abstractions.DataAccess;
using BookLibraryApi.src.Infrastructures.DataAccess.Data;

namespace BookLibraryApi.src.Infrastructures.DataAccess.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
    }

    public async Task<int> SaveChangesAsync(CancellationToken ct)
    {
        return await _context.SaveChangesAsync(ct);
    }
}
