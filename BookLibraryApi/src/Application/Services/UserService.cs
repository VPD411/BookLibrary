using BookLibraryApi.src.Domain.Models;
using BookLibraryApi.src.Application.Abstractions.Services;
using BookLibraryApi.src.Application.Abstractions.DataAccess.Repositories;

namespace BookLibraryApi.src.Application.Services;

public class UserService : IUserService
{
    private readonly IUsersRepository _repository;

    public UserService(IUsersRepository repository)
    {
        _repository = repository;
    }

    public async Task<User?> Authenticate(string username, string password, CancellationToken ct)
    {
        var result = await _repository.GetWithPredicateAsync(u => u.Username == username && u.PasswordHash == password, ct);
        return result;
    }

    public async Task<User> Register(string username, string password, CancellationToken ct)
    {
        var user = new User
        {
            Username = username,
            PasswordHash = password
        };

        var result = await _repository.CreateAsync(user, ct);
        return result;
    }

    public async Task<User?> GetAsync(Guid id, CancellationToken ct)
    {
        var user = await _repository.GetByIdAsync(id, ct);
        return user;
    }
}
