using BLL.DTO.Genre;
using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GenreController : ControllerBase
    {
        private IGenreService genreService;

        public GenreController(IGenreService genreService)
        {
            this.genreService = genreService;
        }

        [HttpGet]
        public async Task<ActionResult<List<GenreReadDto>>> GetAll()
        {
            List<GenreReadDto> genres = await genreService.GetAllAsync();
            return Ok(genres);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            GenreReadDto genre = await genreService.GetByIdAsync(id);

            if (genre == null)
            {
                return NotFound(new { message = $"Genre with ID {id} not found" });
            }

            return Ok(genre);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] GenreCreateDto dto)
        {
            GenreReadDto createdGenre = await genreService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = createdGenre.Id }, createdGenre);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] GenreUpdateDto dto)
        {
            GenreReadDto updatedGenre = await genreService.UpdateAsync(id, dto);

            if (updatedGenre == null)
            {
                return NotFound(new { message = $"Genre with ID {id} not found" });
            }

            return Ok(new { message = "Genre name updated successfully" });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            bool isDeleted = await genreService.DeleteAsync(id);

            if (isDeleted == false)
            {
                return NotFound(new { message = $"Genre with ID {id} not found" });
            }

            return Ok(new { message = "Genre deleted successfully" });
        }

        [HttpGet("books-count")]
        public async Task<ActionResult<Dictionary<string, int>>> GetGenresWithBooksCount()
        {
            Dictionary<string, int> data = await genreService.GetGenresWithBooksCountAsync();
            return Ok(data);
        }
    }
}
