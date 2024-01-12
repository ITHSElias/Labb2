using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Labb2.Model;
using Labb2.DTOs;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Labb2.Extensions;

namespace Labb2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookCopiesInLibraryController : ControllerBase
    {
        private readonly AppDbContext _context;

        public BookCopiesInLibraryController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/BookCopiesInLibrary
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookCopyInLibraryDTO>>> GetBookCopiesInLibrary()
        {
            var bookCopiesInLibrary = await _context.BookCopiesInLibrary.Include(bookCopy => bookCopy.Book).ThenInclude(book => book.Authors).ToListAsync();
            List<BookCopyInLibraryDTO> bookCopyInLibraryDTOs = [];
            foreach(var bookCopyInLibrary in bookCopiesInLibrary)
            {
                bookCopyInLibraryDTOs.Add(bookCopyInLibrary.ToBookCopyInLibraryDTO());
            }
            return bookCopyInLibraryDTOs;
        }

        // GET: api/BookCopiesInLibrary/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BookCopyInLibraryDTO>> GetBookCopyInLibrary(int id)
        {
            var bookCopyInLibrary = await _context.BookCopiesInLibrary.FindAsync(id);

            if (bookCopyInLibrary == null)
            {
                return NotFound();
            }
            _context.Entry(bookCopyInLibrary)
                .Reference(b => b.Book)
                .Load();
            _context.Entry(bookCopyInLibrary.Book)
                .Collection(b => b.Authors)
                .Load();

            return bookCopyInLibrary.ToBookCopyInLibraryDTO();
        }
/*
        // PUT: api/BookCopiesInLibrary/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBookCopyInLibrary(int id, BookCopyInLibrary bookCopyInLibrary)
        {
            if (id != bookCopyInLibrary.Id)
            {
                return BadRequest();
            }

            _context.Entry(bookCopyInLibrary).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookCopyInLibraryExists(id))
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
        // POST: api/BookCopiesInLibrary
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<BookCopyInLibrary>> PostBookCopyInLibrary(CreateBookCopyInLibraryDTO createBookCopyInLibraryDTO)
        {
            var bookCopyInLibrary = new BookCopyInLibrary
            {
                Book = _context.Books.SingleOrDefault(b => b.Isbn == createBookCopyInLibraryDTO.Isbn)! 
            };
            if (bookCopyInLibrary.Book == null)
            {
                return NotFound();
            }
            _context.BookCopiesInLibrary.Add(bookCopyInLibrary);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBookCopyInLibrary", new { id = bookCopyInLibrary.Id }, bookCopyInLibrary);
        }

        // DELETE: api/BookCopiesInLibrary/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBookCopyInLibrary(int id)
        {
            var bookCopyInLibrary = await _context.BookCopiesInLibrary.FindAsync(id);
            if (bookCopyInLibrary == null)
            {
                return NotFound();
            }

            _context.BookCopiesInLibrary.Remove(bookCopyInLibrary);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BookCopyInLibraryExists(int id)
        {
            return _context.BookCopiesInLibrary.Any(e => e.Id == id);
        }
    }
}
