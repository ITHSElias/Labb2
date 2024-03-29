using Labb2.Model;

namespace Labb2.DTOs;

public class CreateBookDTO
{
    public string Isbn { get; set; } = null!; //Needs to be string to allow for a leading zero
    public string Title { get; set; } = null!;
    public int ReleaseYear { get; set; }
    public List<int> AuthorIds { get; set; } = new();
}