using BLL.DTO.Book;
using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/book")]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public async Task<ActionResult<List<BookReadDto>>> GetAll()
        {
            List<BookReadDto> books = await _bookService.GetAllAsync();
            return Ok(books);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            BookReadDto book = await _bookService.GetByIdAsync(id);
            if (book == null)
            {
                return NotFound(new { message = $"Book with ID {id} not found" });
            }

            return Ok(book);
        }

        [HttpGet("by-author/{authorId}")]
        public async Task<ActionResult<List<BookReadDto>>> GetByAuthorId(int authorId)
        {
            List<BookReadDto> books = await _bookService.GetBooksByAuthorIdAsync(authorId);
            if(books.Count == 0)
            {
                return Ok(new { message = "This author doesn`t have any book" });
            }
            
            return Ok(books);
        }

        [HttpGet("by-genre/{genreId}")]
        public async Task<ActionResult<List<BookReadDto>>> GetByGenreId(int genreId)
        {
            List<BookReadDto> books = await _bookService.GetBooksByGenreIdAsync(genreId);
            if (books.Count == 0)
            {
                return Ok(new { message = "Cannot find any book with this genre ID" });
            }

            return Ok(books);
        }

        [HttpGet("latest/{count}")]
        public async Task<ActionResult<List<BookReadDto>>> GetLatestBooks(int count)
        {
            List<BookReadDto> books = await _bookService.GetLatestBooksAsync(count);
            return Ok(books);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] BookCreateDto dto)
        {
            BookReadDto createdBook = await _bookService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = createdBook.Id }, createdBook);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] BookUpdateDto dto)
        {
            try
            {
                await _bookService.UpdateAsync(id, dto);
                return Ok(new { message = "Book updated successfully" });
            }
            catch (Exception ex)
            {
                return NotFound(new { message = $"Update failed: {ex.Message}" });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _bookService.DeleteAsync(id);
                return Ok(new { message = "Book deleted successfully" });
            }
            catch (Exception ex)
            {
                return NotFound(new { message = $"Delete failed: {ex.Message}" });
            }
        }

        //[HttpGet("paged")]
        //public async Task<ActionResult<List<BookReadDto>>> GetPaged([FromQuery] int pageNumber, [FromQuery] int pageSize)
        //{
        //    List<BookReadDto> books = await _bookService.GetPagedBooksAsync(pageNumber, pageSize);
        //    return Ok(books);
        //}
    }
}
