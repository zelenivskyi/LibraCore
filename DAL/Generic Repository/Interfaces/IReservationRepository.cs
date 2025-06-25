using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;

namespace DAL.Generic_Repository.Interfaces
{
    public interface IReservationRepository : IGenericRepository<Reservation>
    {
        Task<List<Reservation>> GetReservationsByUserIdAsync(int userId);
        Task<List<Reservation>> GetReservationsByBookIdAsync(int bookId);
        Task<List<Reservation>> GetActiveReservationsAsync();
        Task<List<Reservation>> GetCompletedReservationsAsync();
        Task<List<Reservation>> GetAllWithDetailsAsync();
        Task<List<Reservation>> GetPagedReservationsAsync(int pageNumber, int pageSize);
        Task<int> GetTotalReservationsCountAsync();
    }
}
