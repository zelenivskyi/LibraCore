using BLL.DTO.Reservation;
using BLL.Filters;
using BLL.Interfaces;
using BLL.Paginate;
using DAL.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReservationController : ControllerBase
    {
        private IReservationService reservationService;

        public ReservationController(IReservationService reservationService)
        {
            this.reservationService = reservationService;
        }

        [HttpGet]
        public async Task<ActionResult<List<ReservationReadDto>>> GetAll()
        {
            List<ReservationReadDto> reservations = await reservationService.GetAllAsync();
            return Ok(reservations);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            ReservationReadDto reservation = await reservationService.GetByIdAsync(id);

            if (reservation == null)
            {
                return NotFound(new { message = $"Reservation with ID {id} not found" });
            }

            return Ok(reservation);
        }

        [HttpGet("by-user/{userId}")]
        public async Task<ActionResult<List<ReservationReadDto>>> GetByUserId(int userId)
        {
            List<ReservationReadDto> reservations = await reservationService.GetReservationsByUserIdAsync(userId);
            if(reservations.Count == 0)
            {
                return Ok(new { message = $"User with ID {userId} doesn`t has any reservation" });
            }

            return Ok(reservations);
        }

        [HttpGet("by-book/{bookId}")]
        public async Task<ActionResult<List<ReservationReadDto>>> GetByBookId(int bookId)
        {
            List<ReservationReadDto> reservations = await reservationService.GetReservationsByBookIdAsync(bookId);
            if (reservations.Count == 0)
            {
                return Ok(new { message = $"Book with ID {bookId} doesn`t has any reservation" });
            }

            return Ok(reservations);
        }

        [HttpGet("active")]
        public async Task<ActionResult<List<ReservationReadDto>>> GetActive()
        {
            List<ReservationReadDto> reservations = await reservationService.GetActiveReservationsAsync();
            return Ok(reservations);
        }

        [HttpGet("completed")]
        public async Task<ActionResult<List<ReservationReadDto>>> GetCompleted()
        {
            List<ReservationReadDto> reservations = await reservationService.GetCompletedReservationsAsync();
            return Ok(reservations);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ReservationCreateDto dto)
        {
            ReservationReadDto createdReservation = await reservationService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = createdReservation.Id }, createdReservation);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ReservationUpdateDto dto)
        {
            ReservationReadDto updatedReservation = await reservationService.UpdateAsync(id, dto);

            if (updatedReservation == null)
            {
                return NotFound(new { message = $"Reservation with ID {id} not found" });
            }

            return Ok(new { message = "Reservation updated successfully" });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            bool isDeleted = await reservationService.DeleteAsync(id);

            if (isDeleted == false)
            {
                return NotFound(new { message = $"Reservation with ID {id} not found" });
            }

            return Ok(new { message = "Reservation deleted successfully" });
        }


        [HttpGet("paged")]
        public async Task<ActionResult<PagedList<ReservationReadDto>>> GetPagedReservations([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var pagedReservations = await reservationService.GetPagedReservationsAsync(pageNumber, pageSize);
            return Ok(pagedReservations);
        }

        [HttpGet("filter")]
        public async Task<ActionResult<PagedList<ReservationReadDto>>> GetFilteredReservations([FromQuery] ReservationFilterDto filterDto,[FromQuery] int pageNumber = 1,[FromQuery] int pageSize = 10)
        {
            if (pageNumber < 1)
            {
                pageNumber = 1;
            }
            if (pageSize < 1)
            {
                pageSize = 10;
            }

            PagedList<ReservationReadDto> filteredReservations = await reservationService.GetFilteredReservationsAsync(filterDto, pageNumber, pageSize);
            return Ok(filteredReservations);
        }
    }
}
