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
    public class ReservationRepository : GenericRepository<Reservation>, IReservationRepository
    {
        public ReservationRepository(DbContext context) : base(context)
        {

        }

        public IQueryable<Reservation> GetQueryable()
        {
            return dbSet.Include(r => r.User).Include(r => r.Book).AsQueryable();
        }
        public async Task<List<Reservation>> GetReservationsByUserIdAsync(int userId)
        {
            return await dbSet
                .Where(reservation => reservation.UserId == userId)
                .Include(reservation => reservation.Book)
                .Include(reservation => reservation.User)
                .ToListAsync();
        }

        public async Task<List<Reservation>> GetReservationsByBookIdAsync(int bookId)
        {
            return await dbSet
                .Where(reservation => reservation.BookId == bookId)
                .Include(reservation => reservation.Book)
                .Include(reservation => reservation.User)
                .ToListAsync();
        }

        public async Task<List<Reservation>> GetActiveReservationsAsync()
        {
            return await dbSet
                .Where(reservation => reservation.ReturnedAt == null && reservation.Status == "Reserved")
                .Include(reservation => reservation.Book)
                .Include(reservation => reservation.User)
                .ToListAsync();
        }

        public async Task<List<Reservation>> GetCompletedReservationsAsync()
        {
            return await dbSet
                .Where(reservation => reservation.ReturnedAt != null || reservation.Status == "Returned")
                .Include(reservation => reservation.Book)
                .Include(reservation => reservation.User)
                .ToListAsync();
        }

        public async Task<List<Reservation>> GetAllWithDetailsAsync()
        {
            return await dbSet
                .Include(reservation => reservation.Book)
                .Include(reservation => reservation.User)
                .ToListAsync();
        }

        public async Task<List<Reservation>> GetPagedReservationsAsync(int pageNumber, int pageSize)
        {
            return await dbSet
                .OrderBy(reservation => reservation.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Include(reservation => reservation.Book)
                .Include(reservation => reservation.User)
                .ToListAsync();
        }

        public async Task<int> GetTotalReservationsCountAsync()
        {
            return await dbSet.CountAsync();
        }

        public async Task<Reservation?> GetByIdWithDetailsAsync(int id)
        {
            return await dbSet
                .Where(reservation => reservation.Id == id)
                .Include(reservation => reservation.Book)
                .Include(reservation => reservation.User)
                .FirstOrDefaultAsync();
        }
    }
}
