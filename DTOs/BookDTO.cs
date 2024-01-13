namespace Labb2.DTOs;

public class BookDTO
{
    public int Id { get; set; }
    public string Isbn { get; set; } = null!; //Needs to be string to allow for a leading zero
    public string Title { get; set; } = null!;
    public int ReleaseYear { get; set; }
    public List<string> Authors { get; set; } = new();
}