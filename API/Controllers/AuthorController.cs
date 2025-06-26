using BLL.DTO.Author;
using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/author")]
    public class AuthorController : ControllerBase
    {
        private IAuthorService authorService;

        public AuthorController(IAuthorService authorService)
        {
            this.authorService = authorService;
        }

        [HttpGet]
        public async Task<ActionResult<List<AuthorReadDto>>> GetAll()
        {
            List<AuthorReadDto> authors = await authorService.GetAllAsync();
            return Ok(authors);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            AuthorReadDto author = await authorService.GetByIdAsync(id);
            if (author == null)
            {
                return NotFound(new { message = $"Author with ID {id} not found" });
            }

            return Ok(author);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AuthorCreateDto dto)
        {
            AuthorReadDto createdAuthor = await authorService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = createdAuthor.Id }, createdAuthor);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] AuthorUpdateDto dto)
        {
            try
            {
                await authorService.UpdateAsync(id, dto);
                return Ok(new { message = "Author updated successfully" });
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
                await authorService.DeleteAsync(id);
                return Ok(new { message = "Author deleted successfully" });
            }
            catch (Exception ex)
            {
                return NotFound(new { message = $"Delete failed: {ex.Message}" });
            }
        }

        [HttpGet("books-count")]
        public async Task<ActionResult<Dictionary<string, int>>> GetAuthorsWithBooksCount()
        {
            Dictionary<string, int> data = await authorService.GetAuthorsWithBooksCountAsync();
            return Ok(data);
        }

        //[HttpGet("paged")]
        //public async Task<ActionResult<List<AuthorReadDto>>> GetPaged([FromQuery] int pageNumber, [FromQuery] int pageSize)
        //{
        //    var authors = await _authorService.GetPagedAuthorsAsync(pageNumber, pageSize);
        //    return Ok(authors);
        //}
    }
}
