using System.ComponentModel.DataAnnotations;

namespace BookLibraryApi.src.Domain.Models;

public class Review
{
    // Основные свойства
    public Guid Id { get; set; }

    [Range(1, 5)]
    public int Rating { get; set; }

    [StringLength(500)]
    public string? Comment { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Навигационные свойства
    public Guid BookId { get; set; } // FK (Foreign Key) Внешний ключ
    public Book? Book { get; set; }

    public Guid UserId { get; set; }
    public User? User { get; set; }

}
