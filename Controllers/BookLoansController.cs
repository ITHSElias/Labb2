using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Labb2.Model;
using Labb2.DTOs;
using Labb2.Extensions;

namespace Labb2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookLoansController : ControllerBase
    {
        private readonly AppDbContext _context;

        public BookLoansController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/BookLoans
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookLoan>>> GetBookLoans()
        {
            return await _context.BookLoans
                .Include(b => b.BookCopy)
                .ThenInclude(b => b.Book)
                .Include(b => b.Customer)
                .ToListAsync();
        }

        // GET: api/BookLoans/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BookLoan>> GetBookLoan(int id)
        {
            var bookLoan = await _context.BookLoans.FindAsync(id);

            if (bookLoan == null)
            {
                return NotFound();
            }

            await _context.Entry(bookLoan)
                .Reference(b => b.Customer)
                .LoadAsync();

            await _context.Entry(bookLoan)
                .Reference(b => b.BookCopy)
                .LoadAsync();

            await _context.Entry(bookLoan.BookCopy)
                .Reference(b => b.Book)
                .LoadAsync();

            return bookLoan;
        }
/*
        // PUT: api/BookLoans/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBookLoan(int id, BookLoan bookLoan)
        {
            if (id != bookLoan.Id)
            {
                return BadRequest();
            }

            _context.Entry(bookLoan).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookLoanExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }
*/
        // POST: api/BookLoans
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("loanbook")]
        public async Task<ActionResult<BookLoan>> LoanBook(LoanBookDTO loanBookDTO)
        {
            var customer = await _context.Customers.FindAsync(loanBookDTO.CustomerId);
            var bookCopy = await _context.BookCopiesInLibrary.FindAsync(loanBookDTO.BookCopyId);
            
            if(customer == null)
            {
                return NotFound(loanBookDTO.CustomerId);
            }
            if(bookCopy == null)
            {
                return NotFound(loanBookDTO.BookCopyId);
            }
            if(!bookCopy.IsAvailable)
            {
                return BadRequest("Book is not available"); 
            }
            if(!customer.HasValidLibraryCard)
            {
                return BadRequest("Customer does not have valid library card");
            }
            
            var bookLoan = loanBookDTO.ToBookLoan(customer, bookCopy);
            _context.BookLoans.Add(bookLoan);
            
            bookCopy.IsAvailable = false;
            _context.Entry(bookCopy).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBookLoan", new { id = bookLoan.Id }, bookLoan);
        }

         // PUT: api/BookLoans/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("returnbook/{id}")]
        public async Task<IActionResult> ReturnBook(int id, int? rating)
        {
            var bookLoan = await _context.BookLoans.FindAsync(id);
            if(bookLoan == null)
            {
                return NotFound();
            }
            if(bookLoan.IsReturned)
            {
                return BadRequest("Book is already returned");
            }
            bookLoan.ReturnDate = DateOnly.FromDateTime(DateTime.Now);
            bookLoan.IsReturned = true;
            if(rating >= 1 && rating <= 10) bookLoan.Rating = rating;
            else bookLoan.Rating = null;
            
            _context.Entry(bookLoan).State = EntityState.Modified;

            await _context.Entry(bookLoan)
                .Reference(b => b.BookCopy)
                .LoadAsync();

            bookLoan.BookCopy.IsAvailable = true;
            _context.Entry(bookLoan.BookCopy).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookLoanExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/BookLoans/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBookLoan(int id)
        {
            var bookLoan = await _context.BookLoans.FindAsync(id);
            if (bookLoan == null)
            {
                return NotFound();
            }

            _context.BookLoans.Remove(bookLoan);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        private bool BookLoanExists(int id)
        {
            return _context.BookLoans.Any(e => e.Id == id);
        }
    }
}
