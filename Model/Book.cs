namespace Labb2.Model;
public class Book
{
    public int Id { get; set; }
    public int Isbn { get; set; }
    public string Name { get; set; } = null!;
    public DateOnly ReleaseYear { get; set; }
    public List<Author> Authors { get; } = new();
}