namespace Labb2.Model;
public class BookLoan
{
    public int Id {get; set;}
    public required LibraryCard LibraryCard {get; set;}
    public required Book Book {get; set;} 
    public DateOnly LoanDate {get; set;}
    public DateOnly? ReturnDate {get; set;}
    //Nu finns det bara en bok per boklån, 
    //möjligtvis kan man tänka sig att ett boklån kan bestå av flera böcker som lånades vid samma tillfälle, 
    //kolla med beställare?
}