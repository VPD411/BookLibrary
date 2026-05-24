using BookLibraryApi.src.Domain.Models;

namespace BookLibraryApi.src.Application.Abstractions.Services;

public interface ITokenService
{
    string GenerateToken(User user);
}
