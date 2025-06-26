using BLL.DTO.Review;
using BLL.Interfaces;
using DAL.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReviewController : ControllerBase
    {
        private IReviewService reviewService;

        public ReviewController(IReviewService reviewService)
        {
            this.reviewService = reviewService;
        }

        [HttpGet]
        public async Task<ActionResult<List<ReviewReadDto>>> GetAll()
        {
            List<ReviewReadDto> reviews = await reviewService.GetAllAsync();
            return Ok(reviews);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            ReviewReadDto review = await reviewService.GetByIdAsync(id);

            if (review == null)
            {
                return NotFound(new { message = $"Review with ID {id} not found" });
            }

            return Ok(review);
        }

        [HttpGet("by-user/{userId}")]
        public async Task<ActionResult<List<ReviewReadDto>>> GetByUserId(int userId)
        {
            List<ReviewReadDto> reviews = await reviewService.GetReviewsByUserIdAsync(userId);
            if(reviews.Count == 0)
            {
                return Ok(new { message = $"User with ID {userId} doesn`t left any review" });
            }
            
            return Ok(reviews);
        }

        [HttpGet("by-book/{bookId}")]
        public async Task<ActionResult<List<ReviewReadDto>>> GetByBookId(int bookId)
        {
            List<ReviewReadDto> reviews = await reviewService.GetReviewsByBookIdAsync(bookId);
            if (reviews.Count == 0)
            {
                return Ok(new { message = $"Book with ID {bookId} doesn`t has any review" });
            }

            return Ok(reviews);
        }

        [HttpGet("average-rating/{bookId}")]
        public async Task<IActionResult> GetAverageRating(int bookId)
        {
            double? averageRating = await reviewService.GetAverageRatingByBookIdAsync(bookId);

            if (averageRating == null)
            {
                return NotFound(new { message = $"No reviews found for Book ID {bookId}" });
            }

            return Ok(new { AverageRating = averageRating });
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ReviewCreateDto dto)
        {
            ReviewReadDto createdReview = await reviewService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = createdReview.Id }, createdReview);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ReviewUpdateDto dto)
        {
            ReviewReadDto review = await reviewService.UpdateAsync(id, dto);

            if (review == null)
            {
                return NotFound(new { message = $"Review with ID {id} not found" });
            }

            return Ok(new { message = "Review updated successfully" });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            bool isDeleted = await reviewService.DeleteAsync(id);

            if (isDeleted == false)
            {
                return NotFound(new { message = $"Review with ID {id} not found" });
            }

            return Ok(new { message = "Review deleted successfully" });
        }
    }
}
