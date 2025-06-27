using BLL.DTO.Book;
using BLL.Filters;
using BLL.Interfaces;
using BLL.Paginate;
using BLL.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/book")]
    public class BookController : ControllerBase
    {
        private IBookService bookService;

        public BookController(IBookService bookService)
        {
            this.bookService = bookService;
        }

        [HttpGet]
        public async Task<ActionResult<List<BookReadDto>>> GetAll()
        {
            List<BookReadDto> books = await bookService.GetAllAsync();
            return Ok(books);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            BookReadDto book = await bookService.GetByIdAsync(id);
            if (book == null)
            {
                return NotFound(new { message = $"Book with ID {id} not found" });
            }

            return Ok(book);
        }

        [HttpGet("latest/{count}")]
        public async Task<ActionResult<List<BookReadDto>>> GetLatestBooks(int count)
        {
            List<BookReadDto> books = await bookService.GetLatestBooksAsync(count);
            return Ok(books);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] BookCreateDto dto)
        {
            BookReadDto createdBook = await bookService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = createdBook.Id }, createdBook);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] BookUpdateDto dto)
        {
            try
            {
                await bookService.UpdateAsync(id, dto);
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
                await bookService.DeleteAsync(id);
                return Ok(new { message = "Book deleted successfully" });
            }
            catch (Exception ex)
            {
                return NotFound(new { message = $"Delete failed: {ex.Message}" });
            }
        }

        [HttpGet("filter")]
        public async Task<ActionResult<PagedList<BookReadDto>>> GetFiltered([FromQuery] BookFilterDto filterDto, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            if (pageNumber < 1)
            {
                pageNumber = 1;
            }
            if (pageSize < 1)
            {
                pageSize = 10;
            }

            PagedList<BookReadDto> filteredBooks = await bookService.GetFilteredBooksAsync(filterDto, pageNumber, pageSize);
            return Ok(filteredBooks);
        }


        [HttpGet("paged")]
        public async Task<ActionResult<PagedList<BookReadDto>>> GetPaged([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            PagedList<BookReadDto> pagedBooks = await bookService.GetPagedBooksAsync(pageNumber, pageSize);
            return Ok(pagedBooks);
        }
    }
}
