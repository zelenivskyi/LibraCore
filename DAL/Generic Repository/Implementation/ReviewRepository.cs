using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;
using DAL.Generic_Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Generic_Repository.Implementation
{
    public class ReviewRepository : GenericRepository<Review>, IReviewRepository
    {
        public ReviewRepository(DbContext context) : base(context)
        {

        }

        public async Task<List<Review>> GetReviewsByUserIdAsync(int userId)
        {
            return await dbSet
                .Where(r => r.UserId == userId)
                .Include(r => r.User)
                .Include(r => r.Book)
                .ToListAsync();
        }

        public async Task<List<Review>> GetReviewsByBookIdAsync(int bookId)
        {
            return await dbSet
                .Where(r => r.BookId == bookId)
                .Include(r => r.User)
                .Include(r => r.Book)
                .ToListAsync();
        }

        public async Task<double?> GetAverageRatingByBookIdAsync(int bookId)
        {
            return await dbSet
                .Where(r => r.BookId == bookId)
                .AverageAsync(r => (double?)r.Rating);
        }
    }
}
