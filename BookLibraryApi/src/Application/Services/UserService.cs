using BookLibraryApi.src.Domain.Models;
using BookLibraryApi.src.Domain.Enums;
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
}
