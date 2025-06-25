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
    public class ReservationRepository : GenericRepository<Reservation>, IReservationRepository
    {
        public ReservationRepository(DbContext context) : base(context)
        {

        }

        public async Task<List<Reservation>> GetReservationsByUserIdAsync(int userId)
        {
            return await dbSet
                .Where(r => r.UserId == userId)
                .Include(r => r.Book)
                .Include(r => r.User)
                .ToListAsync();
        }

        public async Task<List<Reservation>> GetReservationsByBookIdAsync(int bookId)
        {
            return await dbSet
                .Where(r => r.BookId == bookId)
                .Include(r => r.Book)
                .Include(r => r.User)
                .ToListAsync();
        }

        public async Task<List<Reservation>> GetActiveReservationsAsync()
        {
            return await dbSet
                .Where(r => r.ReturnedAt == null && r.Status == "Reserved")
                .Include(r => r.Book)
                .Include(r => r.User)
                .ToListAsync();
        }

        public async Task<List<Reservation>> GetCompletedReservationsAsync()
        {
            return await dbSet
                .Where(r => r.ReturnedAt != null || r.Status == "Returned")
                .Include(r => r.Book)
                .Include(r => r.User)
                .ToListAsync();
        }

        public async Task<List<Reservation>> GetAllWithDetailsAsync()
        {
            return await dbSet
                .Include(r => r.Book)
                .Include(r => r.User)
                .ToListAsync();
        }

        public async Task<List<Reservation>> GetPagedReservationsAsync(int pageNumber, int pageSize)
        {
            return await dbSet
                .OrderBy(r => r.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Include(r => r.Book)
                .Include(r => r.User)
                .ToListAsync();
        }

        public async Task<int> GetTotalReservationsCountAsync()
        {
            return await dbSet.CountAsync();
        }
    }
}
