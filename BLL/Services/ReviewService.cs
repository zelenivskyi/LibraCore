using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DTO.Review;
using BLL.Interfaces;
using DAL.Entities;
using DAL.UOW;

namespace BLL.Services
{
    public class ReviewService : IReviewService
    {
        private IUnitOfWork unitOfWork;

        public ReviewService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<List<ReviewReadDto>> GetAllAsync()
        {
            List<Review> reviews = await unitOfWork.Reviews.GetAllWithDetails();
            List<ReviewReadDto> result = new List<ReviewReadDto>();

            foreach (Review review in reviews)
            {
                ReviewReadDto readDto = new ReviewReadDto
                {
                    Id = review.Id,
                    UserId = review.UserId,
                    UserFullName = review.User.FullName,
                    BookId = review.BookId,
                    BookTitle = review.Book.Title,
                    Rating = review.Rating,
                    Comment = review.Comment,
                    CreatedAt = review.CreatedAt.ToUniversalTime()
                };
                result.Add(readDto);
            }
            return result;
        }

        public async Task<ReviewReadDto> GetByIdAsync(int id)
        {
            Review review = await unitOfWork.Reviews.GetByIdWithDetails(id);
            if (review == null)
            {
                return null;
            }

            ReviewReadDto result = new ReviewReadDto
            {
                Id = review.Id,
                UserId = review.UserId,
                UserFullName = review.User.FullName,
                BookId = review.BookId,
                BookTitle = review.Book.Title,
                Rating = review.Rating,
                Comment = review.Comment,
                CreatedAt = review.CreatedAt.ToUniversalTime()
            };
            return result;
        }

        public async Task<ReviewReadDto> CreateAsync(ReviewCreateDto dto)
        {
            Review review = new Review
            {
                UserId = dto.UserId,
                BookId = dto.BookId,
                Rating = dto.Rating,
                Comment = dto.Comment,
                CreatedAt = DateTime.UtcNow.ToUniversalTime()
            };

            await unitOfWork.Reviews.AddAsync(review);
            await unitOfWork.SaveChangesAsync();

            ReviewReadDto result = new ReviewReadDto
            {
                Id = review.Id,
                UserId = review.UserId,
                UserFullName = review.User.FullName,
                BookId = review.BookId,
                BookTitle = review.Book.Title,
                Rating = review.Rating,
                Comment = review.Comment,
                CreatedAt = review.CreatedAt.ToUniversalTime()
            };
            return result;
        }

        public async Task<ReviewReadDto> UpdateAsync(int id, ReviewUpdateDto dto)
        {
            Review? review = await unitOfWork.Reviews.GetByIdWithDetails(id);
            if (review == null)
            {
                return null;
            }

            review.Rating = dto.Rating;
            review.Comment = dto.Comment;
            unitOfWork.Reviews.Update(review);
            await unitOfWork.SaveChangesAsync();

            Review updatedReview = await unitOfWork.Reviews.GetByIdWithDetails(id);
            ReviewReadDto result = new ReviewReadDto
            {
                Id = updatedReview.Id,
                UserId = updatedReview.UserId,
                UserFullName = updatedReview.User.FullName,
                BookId = updatedReview.BookId,
                BookTitle = updatedReview.Book.Title,
                Rating = updatedReview.Rating,
                Comment = updatedReview.Comment,
                CreatedAt = updatedReview.CreatedAt.ToUniversalTime()
            };
            return result;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            Review? review = await unitOfWork.Reviews.GetByIdAsync(id);
            if (review == null)
            {
                return false;
            }

            unitOfWork.Reviews.Delete(review);
            await unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<List<ReviewReadDto>> GetReviewsByUserIdAsync(int userId)
        {
            List<Review> reviews = await unitOfWork.Reviews.GetReviewsByUserIdAsync(userId);
            List<ReviewReadDto> result = new List<ReviewReadDto>();

            foreach (Review review in reviews)
            {
                ReviewReadDto readDto = new ReviewReadDto
                {
                    Id = review.Id,
                    UserId = review.UserId,
                    UserFullName = review.User.FullName,
                    BookId = review.BookId,
                    BookTitle = review.Book.Title,
                    Rating = review.Rating,
                    Comment = review.Comment,
                    CreatedAt = review.CreatedAt.ToUniversalTime()
                };
                result.Add(readDto);
            }
            return result;
        }

        public async Task<List<ReviewReadDto>> GetReviewsByBookIdAsync(int bookId)
        {
            List<Review> reviews = await unitOfWork.Reviews.GetReviewsByBookIdAsync(bookId);
            List<ReviewReadDto> result = new List<ReviewReadDto>();

            foreach (Review review in reviews)
            {
                ReviewReadDto readDto = new ReviewReadDto
                {
                    Id = review.Id,
                    UserId = review.UserId,
                    UserFullName = review.User.FullName,
                    BookId = review.BookId,
                    BookTitle = review.Book.Title,
                    Rating = review.Rating,
                    Comment = review.Comment,
                    CreatedAt = review.CreatedAt.ToUniversalTime()
                };
                result.Add(readDto);
            }
            return result;
        }

        public async Task<double?> GetAverageRatingByBookIdAsync(int bookId)
        {
            double? result = await unitOfWork.Reviews.GetAverageRatingByBookIdAsync(bookId);
            return result;
        }
    }
}
