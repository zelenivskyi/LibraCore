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
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(DbContext context) : base(context)
        {

        }

        public async Task<List<Reservation>> GetReservationsByUserIdAsync(int userId)
        {
            return await dbSet
                .Where(u => u.Id == userId)
                .SelectMany(u => u.Reservations)
                .Include(r => r.Book)
                .ToListAsync();
        }

        public async Task<List<Review>> GetReviewsByUserIdAsync(int userId)
        {
            return await dbSet
                .Where(u => u.Id == userId)
                .SelectMany(u => u.Reviews)
                .Include(r => r.Book)
                .ToListAsync();
        }

        public async Task<List<User>> GetPagedUsersAsync(int pageNumber, int pageSize)
        {
            return await dbSet
                .OrderBy(u => u.Id)
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
