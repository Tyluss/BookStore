using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookStore.Entities.Models;
using BookStore.Entities.Dtos;

namespace BookStore.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly BookContext _context;

        public BookController(BookContext context)
        {
            _context = context;
        }

        // GET: Books
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookDto>>> GetBooks()
        {
            if (_context.Books == null)
            {
                return NotFound();
            }

            return await _context.Books
           .Select(x => ItemToDTO(x))
           .ToListAsync();
        }

        // GET: Books/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BookDto>> GetBook(long id)
        {
          if (_context.Books == null)
          {
              return NotFound();
          }
            var customer = await _context.Books.FindAsync(id);

            if (customer == null)
            {
                return NotFound();
            }

            return ItemToDTO(customer);
        }

        // PUT: Books/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBook(long id, BookDto customerDTO)
        {
            if (id != customerDTO.id)
            {
                return BadRequest();
            }

            _context.Entry(customerDTO).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(id))
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

        // POST: Books
        [HttpPost]
        public async Task<ActionResult<BookModel>> PostBook(BookDto bookDto)
        {
            if (_context.Books == null)
            {
                return Problem("Entity set 'BookContext.Books' is null.");
            }

            var book = new BookModel
            {
                name = bookDto.name,
                author = bookDto.author,
                genre = bookDto.genre,
                price = bookDto.price
            };

            _context.Books.Add(book);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBook", new { book.id }, book);
        }


        // DELETE: Books/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(long id)
        {
            if (_context.Books == null)
            {
                return NotFound();
            }
            var customer = await _context.Books.FindAsync(id);

            if (customer == null)
            {
                return NotFound();
            }

            _context.Books.Remove(customer);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BookExists(long id)
        {
            return (_context.Books?.Any(e => e.id == id)).GetValueOrDefault();
        }

        private static BookDto ItemToDTO(BookModel book) =>
          new()
          {
              id = (int)book.id,
              name = book.name,
              genre = book.genre,
              price = book.price
          };
    }
}
