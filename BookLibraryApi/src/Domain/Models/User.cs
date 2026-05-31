using BookLibraryApi.src.Domain.Enums;

namespace BookLibraryApi.src.Domain.Models;

public class User
{
    public Guid Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string Roles { get; set; } = "User";

    public ICollection<Review> Reviews { get; set; } = new List<Review>();
}
