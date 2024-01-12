namespace Labb2.DTOs;

public class CreateCustomerDTO
{
    public string SocialSecurityNumber {get; set;} = null!; //Needs to be string to allow for a leading zero
    public string FirstName {get; set;} = null!;
    public string LastName {get; set;} = null!;
    public bool HasValidLibraryCard { get; set; }
}