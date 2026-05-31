namespace BookLibraryApi.src.Domain.Models;

public class Book
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public string Genre { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Year { get; set; }

    public ICollection<Review> Reviews { get; set; } = new List<Review>();
}