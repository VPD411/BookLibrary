namespace BookLibraryApi.src.Application.Abstractions.DataAccess
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync(CancellationToken ct);
    }
}