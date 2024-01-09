using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
namespace Labb2.Model;
public class  AppDbContext : DbContext
{    
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    public DbSet<Author> Authors {get; set;}
    public DbSet<Book> Books {get; set;}
    public DbSet<BookCopyInLibrary> BookCopiesInLibrary {get; set;}
    public DbSet<BookLoan> BookLoans {get; set;}
    public DbSet<Customer> Customers {get; set;}
    public DbSet<LibraryCard> LibraryCards{get; set;}
}
/*
    Tabell från beställare:
    
    Boklån:
        bok_id
        utlånad
        boktitel
        författare
        isbn
        utgivningsår
        betyg
        lånekort
        förnamn
        efternamn
        lånedatum nullable
        returdatum nullable
*/
//Gör til separata filer sen