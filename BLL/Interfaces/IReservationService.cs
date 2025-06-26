using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DTO.Reservation;
using BLL.Filters;
using BLL.Paginate;

namespace BLL.Interfaces
{
    public interface IReservationService
    {
        Task<List<ReservationReadDto>> GetAllAsync();
        Task<ReservationReadDto> GetByIdAsync(int id);
        Task<ReservationReadDto> CreateAsync(ReservationCreateDto dto);
        Task<ReservationReadDto> UpdateAsync(int id, ReservationUpdateDto dto);
        Task<bool> DeleteAsync(int id);
        Task<PagedList<ReservationReadDto>> GetFilteredReservationsAsync(ReservationFilterDto filterDto, int pageNumber, int pageSize);
        Task<List<ReservationReadDto>> GetReservationsByUserIdAsync(int userId);
        Task<List<ReservationReadDto>> GetReservationsByBookIdAsync(int bookId);
        Task<List<ReservationReadDto>> GetActiveReservationsAsync();
        Task<List<ReservationReadDto>> GetCompletedReservationsAsync();
        Task<PagedList<ReservationReadDto>> GetPagedReservationsAsync(int pageNumber, int pageSize);
    }
}
