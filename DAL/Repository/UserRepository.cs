using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;
using DAL.Generic_Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Implementation
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(DbContext context) : base(context)
        {

        }

        public async Task<bool> PhoneNumberExistsAsync(string phoneNumber, int? excludeUserId = null)
        {
            var query = dbSet.Where(u => u.PhoneNumber.ToLower() == phoneNumber.ToLower());

            if (excludeUserId.HasValue)
            {
                query = query.Where(u => u.Id != excludeUserId.Value);
            }

            return await query.AnyAsync();
        }

        public IQueryable<User> GetQueryable()
        {
            return dbSet.AsQueryable();
        }

        public async Task<List<Reservation>> GetReservationsByUserIdAsync(int userId)
        {
            return await dbSet
                .Where(user => user.Id == userId)
                .SelectMany(user => user.Reservations)
                .Include(book => book.Book)
                .ToListAsync();
        }

        public async Task<List<Review>> GetReviewsByUserIdAsync(int userId)
        {
            return await dbSet
                .Where(user => user.Id == userId)
                .SelectMany(user => user.Reviews)
                .Include(book => book.Book)
                .ToListAsync();
        }

        public async Task<List<User>> GetPagedUsersAsync(int pageNumber, int pageSize)
        {
            return await dbSet
                .OrderBy(user => user.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<int> GetTotalUsersCountAsync()
        {
            return await dbSet.CountAsync();
        }
    }
}
