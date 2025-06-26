using BLL.DTO.User;
using BLL.Filters;
using BLL.Interfaces;
using BLL.Paginate;
using BLL.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<List<UserReadDto>>> GetAll()
        {
            List<UserReadDto> users = await userService.GetAllAsync();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            UserReadDto user = await userService.GetByIdAsync(id);
            if (user == null)
            {
                return NotFound(new { message = $"User with ID {id} not found" });
            }
            return Ok(user);
        }

        [HttpPost("create/librarian")]
        public async Task<IActionResult> CreateLibrarian([FromBody] UserCreateDto dto)
        {
            UserReadDto createdUser = await userService.CreateLibrarianAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = createdUser.Id }, createdUser);
        }

        [HttpPost("create/admin")]
        public async Task<IActionResult> CreateAdmin([FromBody] UserCreateDto dto)
        {
            UserReadDto createdUser = await userService.CreateAdminAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = createdUser.Id }, createdUser);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UserUpdateDto dto)
        {
            UserReadDto updatedUser = await userService.UpdateAsync(id, dto);
            if (updatedUser == null)
            {
                return NotFound(new { message = $"User with ID {id} not found" });
            }
            return Ok(new { message = "User updated successfully" });
        }

        [HttpGet("paged")]
        public async Task<ActionResult<PagedList<UserReadDto>>> GetPagedUsers([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var pagedUsers = await userService.GetPagedUsersAsync(pageNumber, pageSize);
            return Ok(pagedUsers);
        }

        [HttpGet("filter")]
        public async Task<ActionResult<PagedList<UserReadDto>>> GetFilteredUsers([FromQuery] UserFilterDto filterDto, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            if (pageNumber < 1)
            {
                pageNumber = 1;
            }
            if (pageSize < 1)
            {
                pageSize = 10;
            }

            PagedList<UserReadDto> filteredUsers = await userService.GetFilteredUsersAsync(filterDto, pageNumber, pageSize);
            return Ok(filteredUsers);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            bool isDeleted = await userService.DeleteAsync(id);
            if (isDeleted == false)
            {
                return NotFound(new { message = $"User with ID {id} not found" });
            }
            return Ok(new { message = "User deleted successfully" });
        }
    }
}
