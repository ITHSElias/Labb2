using Labb2.DTOs;
using Labb2.Model;
using Microsoft.EntityFrameworkCore;

namespace Labb2.Extensions;
public static class Extensions
{
    public static Author ToAuthor(this CreateAuthorDTO createAuthorDTO)
    {
        return new Author
        {
            FirstName = createAuthorDTO.FirstName,
            LastName = createAuthorDTO.LastName
        };
    }
    public static Book ToBook(this CreateBookDTO createBookDTO)
    {
        var book  = new Book
        {
            Isbn = createBookDTO.Isbn,
            Title = createBookDTO.Title,
            ReleaseDate = new DateOnly(createBookDTO.ReleaseYear, 1, 1),
        };
        return book;
    }
    public static Customer ToCustomer(this CreateCustomerDTO createCustomerDTO)
    {
        return new Customer
        {
            SocialSecurityNumber = createCustomerDTO.SocialSecurityNumber,
            FirstName = createCustomerDTO.FirstName,
            LastName = createCustomerDTO.LastName,
            HasValidLibraryCard = createCustomerDTO.HasValidLibraryCard
        };
    }
    public static BookLoan ToBookLoan(this LoanBookDTO loanBookDTO, Customer customer, BookCopyInLibrary bookCopy)
    {
        return new BookLoan
        {
            Customer = customer,
            BookCopy = bookCopy,
            LoanDate = DateOnly.FromDateTime(DateTime.Now),
        };
    }
    public static BookDTO ToBookDTO(this Book book)
    {
        var bookDTO = new BookDTO
        {
            Id = book.Id,
            Isbn = book.Isbn,
            Title = book.Title,
            ReleaseYear = book.ReleaseDate.Year
        };
        foreach(var author in book.Authors)
        {
            bookDTO.Authors.Add($"{author.FirstName} {author.LastName}");
        }
        return bookDTO;
    }
    public static AuthorDTO ToAuthorDTO(this Author author)
    {
        var authorDTO = new AuthorDTO
        {
            Id = author.Id,
            FirstName = author.FirstName,
            LastName = author.LastName
        };
        foreach (var book in author.Books)
        {
            authorDTO.BookTitles.Add(book.Title);
        }
        return authorDTO;
    }
    public static BookCopyInLibraryDTO ToBookCopyInLibraryDTO(this BookCopyInLibrary bookCopyInLibrary)
    {
        return new BookCopyInLibraryDTO
        {
            Id = bookCopyInLibrary.Id,
            BookDTO = bookCopyInLibrary.Book.ToBookDTO(),
            IsAvailable = bookCopyInLibrary.IsAvailable
        };
    }

}