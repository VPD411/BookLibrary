using BookLibraryApi.src.Domain.Models;

namespace BookLibraryApi.src.Application.Abstractions.Services;

public interface IUserService
{
    Task<User?> Authenticate(string username, string password, CancellationToken ct);
}
