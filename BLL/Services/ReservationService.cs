using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DTO.Reservation;
using BLL.Filters;
using BLL.Interfaces;
using BLL.Paginate;
using DAL.Entities;
using DAL.UOW;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services
{
    public class ReservationService : IReservationService
    {
        private IUnitOfWork unitOfWork;

        public ReservationService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<List<ReservationReadDto>> GetAllAsync()
        {
            List<Reservation> reservations = await unitOfWork.Reservations.GetAllWithDetailsAsync();
            List<ReservationReadDto> result = new List<ReservationReadDto>();

            foreach (Reservation reservation in reservations)
            {
                ReservationReadDto readDto = new ReservationReadDto
                {
                    Id = reservation.Id,
                    UserId = reservation.UserId,
                    UserFullName = reservation.User.FullName,
                    BookId = reservation.BookId,
                    BookTitle = reservation.Book.Title,
                    ReservedAt = reservation.ReservedAt.ToUniversalTime(),
                    ReturnedAt = reservation.ReturnedAt?.ToUniversalTime(),
                    Status = reservation.Status
                };
                result.Add(readDto);
            }
            return result;
        }

        public async Task<ReservationReadDto> GetByIdAsync(int id)
        {
            Reservation reservation = await unitOfWork.Reservations.GetByIdWithDetailsAsync(id);
            if (reservation == null)
            {
                return null;
            }

            ReservationReadDto result = new ReservationReadDto
            {
                Id = reservation.Id,
                UserId = reservation.UserId,
                UserFullName = reservation.User.FullName,
                BookId = reservation.BookId,
                BookTitle = reservation.Book.Title,
                ReservedAt = reservation.ReservedAt.ToUniversalTime(),
                ReturnedAt = reservation.ReturnedAt?.ToUniversalTime(),
                Status = reservation.Status
            };
            return result;
        }

        public async Task<ReservationReadDto> CreateAsync(ReservationCreateDto dto)
        {
            Reservation reservation = new Reservation
            {
                UserId = dto.UserId,
                BookId = dto.BookId,
                ReservedAt = DateTime.UtcNow.ToUniversalTime(),
                Status = "Reserved"
            };

            await unitOfWork.Reservations.AddAsync(reservation);
            await unitOfWork.SaveChangesAsync();
            List<Reservation> reservations = await unitOfWork.Reservations.GetAllWithDetailsAsync();
            reservation = reservations.Last();

            ReservationReadDto result = new ReservationReadDto
            {
                Id = reservation.Id,
                UserId = reservation.UserId,
                UserFullName = reservation.User.FullName,
                BookId = reservation.BookId,
                BookTitle = reservation.Book.Title,
                ReservedAt = reservation.ReservedAt.ToUniversalTime(),
                ReturnedAt = reservation.ReturnedAt?.ToUniversalTime(),
                Status = reservation.Status
            };
            return result;
        }

        public async Task<ReservationReadDto> UpdateAsync(int id, ReservationUpdateDto dto)
        {
            Reservation existingReservation = await unitOfWork.Reservations.GetByIdWithDetailsAsync(id);
            if (existingReservation == null)
            {
                return null;
            }

            existingReservation.ReturnedAt = dto.ReturnedAt.ToUniversalTime();
            existingReservation.Status = "Returned";
            unitOfWork.Reservations.Update(existingReservation);
            await unitOfWork.SaveChangesAsync();

            ReservationReadDto result = new ReservationReadDto
            {
                Id = existingReservation.Id,
                UserId = existingReservation.UserId,
                UserFullName = existingReservation.User.FullName,
                BookId = existingReservation.BookId,
                BookTitle = existingReservation.Book.Title,
                ReservedAt = existingReservation.ReservedAt.ToUniversalTime(),
                ReturnedAt = existingReservation.ReturnedAt?.ToUniversalTime(),
                Status = existingReservation.Status
            };
            return result;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            Reservation reservation = await unitOfWork.Reservations.GetByIdWithDetailsAsync(id);
            if (reservation == null)
            {
                return false;
            }

            unitOfWork.Reservations.Delete(reservation);
            await unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<List<ReservationReadDto>> GetReservationsByUserIdAsync(int userId)
        {
            List<Reservation> reservations = await unitOfWork.Reservations.GetReservationsByUserIdAsync(userId);
            List<ReservationReadDto> result = new List<ReservationReadDto>();

            foreach (Reservation reservation in reservations)
            {
                ReservationReadDto readDto = new ReservationReadDto
                {
                    Id = reservation.Id,
                    UserId = reservation.UserId,
                    UserFullName = reservation.User.FullName,
                    BookId = reservation.BookId,
                    BookTitle = reservation.Book.Title,
                    ReservedAt = reservation.ReservedAt.ToUniversalTime(),
                    ReturnedAt = reservation.ReturnedAt?.ToUniversalTime(),
                    Status = reservation.Status
                };
                result.Add(readDto);
            }
            return result;
        }

        public async Task<List<ReservationReadDto>> GetReservationsByBookIdAsync(int bookId)
        {
            List<Reservation> reservations = await unitOfWork.Reservations.GetReservationsByBookIdAsync(bookId);
            List<ReservationReadDto> result = new List<ReservationReadDto>();

            foreach (Reservation reservation in reservations)
            {
                ReservationReadDto readDto = new ReservationReadDto
                {
                    Id = reservation.Id,
                    UserId = reservation.UserId,
                    UserFullName = reservation.User.FullName,
                    BookId = reservation.BookId,
                    BookTitle = reservation.Book.Title,
                    ReservedAt = reservation.ReservedAt.ToUniversalTime(),
                    ReturnedAt = reservation.ReturnedAt?.ToUniversalTime(),
                    Status = reservation.Status
                };
                result.Add(readDto);
            }
            return result;
        }

        public async Task<List<ReservationReadDto>> GetActiveReservationsAsync()
        {
            List<Reservation> reservations = await unitOfWork.Reservations.GetActiveReservationsAsync();
            List<ReservationReadDto> result = new List<ReservationReadDto>();

            foreach (Reservation reservation in reservations)
            {
                ReservationReadDto readDto = new ReservationReadDto
                {
                    Id = reservation.Id,
                    UserId = reservation.UserId,
                    UserFullName = reservation.User.FullName,
                    BookId = reservation.BookId,
                    BookTitle = reservation.Book.Title,
                    ReservedAt = reservation.ReservedAt.ToUniversalTime(),
                    ReturnedAt = reservation.ReturnedAt?.ToUniversalTime(),
                    Status = reservation.Status
                };
                result.Add(readDto);
            }
            return result;
        }

        public async Task<List<ReservationReadDto>> GetCompletedReservationsAsync()
        {
            List<Reservation> reservations = await unitOfWork.Reservations.GetCompletedReservationsAsync();
            List<ReservationReadDto> result = new List<ReservationReadDto>();

            foreach (Reservation reservation in reservations)
            {
                ReservationReadDto readDto = new ReservationReadDto
                {
                    Id = reservation.Id,
                    UserId = reservation.UserId,
                    UserFullName = reservation.User.FullName,
                    BookId = reservation.BookId,
                    BookTitle = reservation.Book.Title,
                    ReservedAt = reservation.ReservedAt.ToUniversalTime(),
                    ReturnedAt = reservation.ReturnedAt?.ToUniversalTime(),
                    Status = reservation.Status
                };
                result.Add(readDto);
            }
            return result;
        }

        public async Task<PagedList<ReservationReadDto>> GetFilteredReservationsAsync(ReservationFilterDto filterDto, int pageNumber, int pageSize)
        {
            IQueryable<Reservation> query = unitOfWork.Reservations.GetQueryable();

            if (!string.IsNullOrEmpty(filterDto.Status))
            {
                string statusFilter = filterDto.Status.ToLower().Trim();
                query = query.Where(r => r.Status.ToLower().Contains(statusFilter));
            }
            if (filterDto.UserId.HasValue)
            {
                query = query.Where(r => r.UserId == filterDto.UserId.Value);
            }
            if (filterDto.BookId.HasValue)
            {
                query = query.Where(r => r.BookId == filterDto.BookId.Value);
            }
            if (filterDto.ReservedFrom.HasValue)
            {
                query = query.Where(r => r.ReservedAt >= filterDto.ReservedFrom.Value);
            }
            if (filterDto.ReservedTo.HasValue)
            {
                query = query.Where(r => r.ReservedAt <= filterDto.ReservedTo.Value);
            }
            string? sortBy = filterDto.SortBy?.ToLower().Trim();

            if (string.IsNullOrEmpty(sortBy) || sortBy == "id")
            {
                if (filterDto.SortDescending)
                {
                    query = query.OrderByDescending(r => r.Id);
                }
                else
                {
                    query = query.OrderBy(r => r.Id);
                }
            }
            else if (sortBy == "status")
            {
                if (filterDto.SortDescending)
                {
                    query = query.OrderByDescending(r => r.Status);
                }
                else
                {
                    query = query.OrderBy(r => r.Status);
                }
            }
            else if (sortBy == "reservedat")
            {
                if (filterDto.SortDescending)
                {
                    query = query.OrderByDescending(r => r.ReservedAt);
                }
                else
                {
                    query = query.OrderBy(r => r.ReservedAt);
                }
            }
            else
            {
                if (filterDto.SortDescending)
                {
                    query = query.OrderByDescending(r => r.Id);
                }
                else
                {
                    query = query.OrderBy(r => r.Id);
                }
            }

            int totalCount = await query.CountAsync();

            List<Reservation> reservations = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Include(r => r.User)  
                .Include(r => r.Book) 
                .ToListAsync();

            List<ReservationReadDto> dtos = new List<ReservationReadDto>();

            foreach (Reservation reservation in reservations)
            {
                ReservationReadDto dto = new ReservationReadDto
                {
                    Id = reservation.Id,
                    UserId = reservation.UserId,
                    UserFullName = reservation.User.FullName,
                    BookId = reservation.BookId,
                    BookTitle = reservation.Book.Title,
                    ReservedAt = reservation.ReservedAt.ToUniversalTime(),
                    ReturnedAt = reservation.ReturnedAt?.ToUniversalTime(),
                    Status = reservation.Status
                };
                dtos.Add(dto);
            }

            return new PagedList<ReservationReadDto>(dtos, totalCount, pageNumber, pageSize);
        }

        public async Task<PagedList<ReservationReadDto>> GetPagedReservationsAsync(int pageNumber, int pageSize)
        {
            List<Reservation> reservations = await unitOfWork.Reservations.GetPagedReservationsAsync(pageNumber, pageSize);
            int totalCount = await unitOfWork.Reservations.GetTotalReservationsCountAsync();

            List<ReservationReadDto> dtos = new List<ReservationReadDto>();

            foreach (Reservation reservation in reservations)
            {
                ReservationReadDto dto = new ReservationReadDto
                {
                    Id = reservation.Id,
                    UserId = reservation.UserId,
                    UserFullName = reservation.User.FullName,
                    BookId = reservation.BookId,
                    BookTitle = reservation.Book.Title,
                    ReservedAt = reservation.ReservedAt.ToUniversalTime(),
                    ReturnedAt = reservation.ReturnedAt?.ToUniversalTime(),
                    Status = reservation.Status
                };
                dtos.Add(dto);
            }

            return new PagedList<ReservationReadDto>(dtos, totalCount, pageNumber, pageSize);
        }
    }
}
