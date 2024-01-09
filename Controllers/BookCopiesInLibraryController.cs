using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Labb2.Model;

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
        public async Task<ActionResult<IEnumerable<BookCopyInLibrary>>> GetBookCopiesInLibrary()
        {
            return await _context.BookCopiesInLibrary.ToListAsync();
        }

        // GET: api/BookCopiesInLibrary/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BookCopyInLibrary>> GetBookCopyInLibrary(int id)
        {
            var bookCopyInLibrary = await _context.BookCopiesInLibrary.FindAsync(id);

            if (bookCopyInLibrary == null)
            {
                return NotFound();
            }

            return bookCopyInLibrary;
        }

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

        // POST: api/BookCopiesInLibrary
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<BookCopyInLibrary>> PostBookCopyInLibrary(BookCopyInLibrary bookCopyInLibrary)
        {
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
