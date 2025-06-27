using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DTO.Review;

namespace BLL.Interfaces
{
    public interface IReviewService
    {
        Task<List<ReviewReadDto>> GetAllAsync();
        Task<ReviewReadDto> GetByIdAsync(int id);
        Task<ReviewReadDto> CreateAsync(ReviewCreateDto dto);
        Task<ReviewReadDto> UpdateAsync(int id, ReviewUpdateDto dto);
        Task<bool> DeleteAsync(int id);
        Task<double?> GetAverageRatingByBookIdAsync(int bookId);
    }
}
