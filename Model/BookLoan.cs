using System.ComponentModel.DataAnnotations;

namespace Labb2.Model;
public class BookLoan
{
    public int Id { get; set; }
    public required Customer Customer { get; set; }
    public required BookCopyInLibrary BookCopy { get; set; } 
    public DateOnly LoanDate { get; set; }
    public DateOnly? ReturnDate { get; set; }
    public bool IsReturned { get; set; } = false;
    [Range(1,10)]
    public int? Rating { get; set; }
}