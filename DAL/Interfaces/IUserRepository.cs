﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;

namespace DAL.Generic_Repository.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<bool> PhoneNumberExistsAsync(string phoneNumber, int? excludeUserId = null);
        IQueryable<User> GetQueryable();
        Task<List<Reservation>> GetReservationsByUserIdAsync(int userId);
        Task<List<Review>> GetReviewsByUserIdAsync(int userId);
        Task<List<User>> GetPagedUsersAsync(int pageNumber, int pageSize);
        Task<int> GetTotalUsersCountAsync();
    }
}
