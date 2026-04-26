using System.ComponentModel.DataAnnotations;

namespace BookLibraryApi.src.Domain.DTOs;

/// <summary>
/// Модель для обновления Book
/// </summary>
public class UpdateBookRequest
{
    [Required(ErrorMessage = "Название обязательно")]
    [StringLength(100, MinimumLength = 1, ErrorMessage = "Название должно быть от 1 до 100 символов")]
    public string Title { get; set; } = string.Empty;

    [Required(ErrorMessage = "Автор обязателен")]
    [StringLength(50, ErrorMessage = "Имя автора не должно превышать 50 символов.")]
    public string Author { get; set; } = string.Empty;

    [Required(ErrorMessage = "Жанр обязателен")]
    public string Genre { get; set; } = string.Empty;

    [Range(0, 100000, ErrorMessage = "Цена должно быть от 0 до 100000")]
    [Required(ErrorMessage = "Цена обязательна")]
    public decimal Price { get; set; }

    [Range(0, 2026, ErrorMessage = "Год выхода должен быть от 0 до 2026")]
    [Required(ErrorMessage = "Год обязателен")]
    public int Year { get; set; }
}