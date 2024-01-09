namespace Labb2.Model;
public class LibraryCard
{
    public int Id {get; set;}
    public required Customer Customer {get; set;}
    public DateOnly ExpirationDate {get; set;}

    //Vad behövs för denna? Räcker det jag har eller ska det vara mer? Mindre?
}