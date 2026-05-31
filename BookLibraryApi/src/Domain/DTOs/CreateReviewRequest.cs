using System.ComponentModel.DataAnnotations;

namespace BookLibraryApi.src.Domain.DTOs;

public class CreateReviewRequest
{
    [Required]
    [Range(1, 5)]
    public int Rating { get; set; }

    [StringLength(500)]
    public string? Comment { get; set; }

    [Required]
    public Guid BookId { get; set; }

    [Required]
    public Guid UserId { get; set; }
}
