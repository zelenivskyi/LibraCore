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
        Task<bool> ReviewExistsAsync(int userId, int bookId);
        Task<List<Review>> GetAllWithDetails();
        Task<Review> GetByIdWithDetails(int id);
        Task<double?> GetAverageRatingByBookIdAsync(int bookId);
    }
}
