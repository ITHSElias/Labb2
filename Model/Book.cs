namespace Labb2.Model;
public class Book
{
    public int Id { get; set; }
    public string Isbn { get; set; } = null!; //Needs to be string to allow for a leading zero
    public string Title { get; set; } = null!;
    public DateOnly ReleaseDate { get; set; }
    public List<Author> Authors { get; } = new();
}