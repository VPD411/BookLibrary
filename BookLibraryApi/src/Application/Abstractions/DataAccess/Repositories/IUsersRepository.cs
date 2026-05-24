using BookLibraryApi.src.Domain.Models;
using System.Linq.Expressions;

namespace BookLibraryApi.src.Application.Abstractions.DataAccess.Repositories
{
    public interface IUsersRepository
    {
        Task<User> CreateAsync(User user, CancellationToken ct);
        void Delete(User user);
        Task<User?> GetWithPredicateAsync(Expression<Func<User, bool>> predicate, CancellationToken ct);
        Task<IEnumerable<User>> GetAllAsync(CancellationToken ct);
        Task<User?> GetByIdAsync(Guid id, CancellationToken ct);
        User Update(User user);
    }
}