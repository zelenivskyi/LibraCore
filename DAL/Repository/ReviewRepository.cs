using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;
using DAL.Generic_Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Implementation
{
    public class ReviewRepository : GenericRepository<Review>, IReviewRepository
    {
        public ReviewRepository(DbContext context) : base(context)
        {

        }

        public async Task<List<Review>> GetReviewsByUserIdAsync(int userId)
        {
            return await dbSet
                .Where(review => review.UserId == userId)
                .Include(review => review.User)
                .Include(review => review.Book)
                .ToListAsync();
        }

        public async Task<List<Review>> GetReviewsByBookIdAsync(int bookId)
        {
            return await dbSet
                .Where(review => review.BookId == bookId)
                .Include(review => review.User)
                .Include(review => review.Book)
                .ToListAsync();
        }

        public async Task<double?> GetAverageRatingByBookIdAsync(int bookId)
        {
            return await dbSet
                .Where(review => review.BookId == bookId)
                .AverageAsync(review => (double?)review.Rating);
        }

        public async Task<List<Review>> GetAllWithDetails()
        {
            return await dbSet
                .Include(review => review.User)
                .Include(review => review.Book)
                .ToListAsync();
        }

        public async Task<Review?> GetByIdWithDetails(int id)
        {
            return await dbSet
                .Where(review => review.Id == id)
                .Include(review => review.User)
                .Include(review => review.Book)
                .FirstOrDefaultAsync(); 
        }
    }
}
