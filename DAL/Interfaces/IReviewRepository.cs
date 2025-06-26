using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;

namespace DAL.Generic_Repository.Interfaces
{
    public interface IReviewRepository : IGenericRepository<Review>
    {
        Task<List<Review>> GetAllWithDetails();
        Task<Review> GetByIdWithDetails(int id);
        Task<List<Review>> GetReviewsByUserIdAsync(int userId);
        Task<List<Review>> GetReviewsByBookIdAsync(int bookId);
        Task<double?> GetAverageRatingByBookIdAsync(int bookId);
    }
}
