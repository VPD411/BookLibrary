using System.ComponentModel.DataAnnotations;

namespace BookLibraryApi.DTOs;

public class CreateBookRequest
{
    [Required]
    public string Title { get; set; } = string.Empty;

    [Required]
    public string Author { get; set; } = string.Empty;

    [Required]
    public string Genre { get; set; } = string.Empty;

    [Range(0, 100000)]
    [Required]
    public decimal Price { get; set; }

    [Required]
    public int Year { get; set; }
}

