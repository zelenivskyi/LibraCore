using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DTO.Reservation;
using BLL.Interfaces;
using DAL.Entities;
using DAL.UOW;

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
            existingReservation.Status = dto.Status;
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

        public async Task<List<ReservationReadDto>> GetPagedReservationsAsync(int pageNumber, int pageSize)
        {
            throw new NotImplementedException();
        }

        private async Task<int> GetTotalReservationsCountAsync()
        {
            int result = await unitOfWork.Reservations.GetTotalReservationsCountAsync();
            return result;
        }
    }
}
