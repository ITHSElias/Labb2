namespace Labb2.DTOs;

public class BookCopyInLibraryDTO
{
    public int Id { get; set; }
    public BookDTO BookDTO { get; set; } = null!;
    public bool IsAvailable { get; set; }
}