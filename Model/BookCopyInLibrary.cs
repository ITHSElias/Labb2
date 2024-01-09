namespace Labb2.Model;
public class BookCopyInLibrary
{
    public int Id {get; set;}
    public required Book Book {get; set;}
    public bool IsAvailable {get; set;}
}