namespace BookLibraryApi.src.Domain.DTOs;

/// <summary>
/// Модель для возвращения пользователю Book
/// </summary>
public class BookResponse
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public string Genre { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Year { get; set; }
}