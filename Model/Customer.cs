namespace Labb2.Model;
public class Customer
{
    public int Id {get; set;}
    public int SocialSecurityNumber {get; set;} //Behövs eller inte?
    public string FirstName {get; set;} = null!;
    public string LastName {get; set;} = null!;
    //Hur ska kunder och lånekort hanteras? Man vill ju att en kund ska kunna ha flera lånekort i längden,
    //när ett lånekort har gått ut så ska ett ny kunnas skapas, däremot ska en kund bara ha ett giltigt 
    //lånekort  i taget. Kolla med beställare   
}