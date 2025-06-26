using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DTO.User;
using BLL.Filters;
using BLL.Interfaces;
using BLL.Paginate;
using DAL.Entities;
using DAL.UOW;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services
{
    public class UserService : IUserService
    {
        private IUnitOfWork unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<List<UserReadDto>> GetAllAsync()
        {
            List<User> users = await unitOfWork.Users.GetAllAsync();
            List<UserReadDto> result = new List<UserReadDto>();

            foreach (User user in users)
            {
                UserReadDto readDto = new UserReadDto
                {
                    Id = user.Id,
                    FullName = user.FullName,
                    PhoneNumber = user.PhoneNumber,
                    Role = user.Role,
                    RegisteredAt = user.RegisteredAt.ToUniversalTime()
                };
                result.Add(readDto);
            }
            return result;
        }

        public async Task<UserReadDto> GetByIdAsync(int id)
        {
            User? user = await unitOfWork.Users.GetByIdAsync(id);
            if (user == null)
            {
                return null;
            }

            UserReadDto result = new UserReadDto
            {
                Id = user.Id,
                FullName = user.FullName,
                PhoneNumber = user.PhoneNumber,
                Role = user.Role,
                RegisteredAt = user.RegisteredAt.ToUniversalTime()
            };
            return result;
        }

        public async Task<UserReadDto> CreateLibrarianAsync(UserCreateDto dto)
        {
            User user = new User
            {
                FullName = dto.FullName,
                PhoneNumber = dto.PhoneNumber,
                Password = dto.Password,
                RegisteredAt = DateTime.UtcNow.ToUniversalTime(),
                Role = "Librarian"
            };

            await unitOfWork.Users.AddAsync(user);
            await unitOfWork.SaveChangesAsync();

            UserReadDto result = new UserReadDto
            {
                Id = user.Id,
                FullName = user.FullName,
                PhoneNumber = user.PhoneNumber,
                Role = user.Role,
                RegisteredAt = user.RegisteredAt.ToUniversalTime()
            };
            return result;
        }

        public async Task<UserReadDto> UpdateAsync(int id, UserUpdateDto dto)
        {
            User? existingUser = await unitOfWork.Users.GetByIdAsync(id);
            if (existingUser == null)
            {
                return null;
            }

            existingUser.FullName = dto.FullName;
            existingUser.PhoneNumber = dto.PhoneNumber;
            existingUser.Password = dto.Password;
            unitOfWork.Users.Update(existingUser);
            await unitOfWork.SaveChangesAsync();

            UserReadDto result = new UserReadDto
            {
                Id = existingUser.Id,
                FullName = existingUser.FullName,
                PhoneNumber = existingUser.PhoneNumber,
                Role = existingUser.Role,
                RegisteredAt = existingUser.RegisteredAt.ToUniversalTime()
            };
            return result;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            User? user = await unitOfWork.Users.GetByIdAsync(id);
            if (user == null)
            {
                return false;
            }

            unitOfWork.Users.Delete(user);
            await unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<PagedList<UserReadDto>> GetPagedUsersAsync(int pageNumber, int pageSize)
        {
            List<User> users = await unitOfWork.Users.GetPagedUsersAsync(pageNumber, pageSize);
            int totalCount = await unitOfWork.Users.GetTotalUsersCountAsync();
            List<UserReadDto> dtos = new List<UserReadDto>();

            foreach (User user in users)
            {
                UserReadDto dto = new UserReadDto
                {
                    Id = user.Id,
                    FullName = user.FullName,
                    PhoneNumber = user.PhoneNumber,
                    Role = user.Role,
                    RegisteredAt = user.RegisteredAt.ToUniversalTime()
                };
                dtos.Add(dto);
            }

            return new PagedList<UserReadDto>(dtos, totalCount, pageNumber, pageSize);
        }

        public async Task<PagedList<UserReadDto>> GetFilteredUsersAsync(UserFilterDto filterDto, int pageNumber, int pageSize)
        {
            IQueryable<User> query = unitOfWork.Users.GetQueryable();

            if (!string.IsNullOrEmpty(filterDto.FullName))
            {
                string fullNameFilter = filterDto.FullName.ToLower().Trim();
                query = query.Where(u => u.FullName.ToLower().Contains(fullNameFilter));
            }

            if (!string.IsNullOrEmpty(filterDto.PhoneNumber))
            {
                string phoneFilter = filterDto.PhoneNumber.ToLower().Trim();
                query = query.Where(u => u.PhoneNumber.ToLower().Contains(phoneFilter));
            }

            if (!string.IsNullOrEmpty(filterDto.Role))
            {
                string roleFilter = filterDto.Role.ToLower().Trim();
                query = query.Where(u => u.Role.ToLower().Contains(roleFilter));
            }
            string? sortBy = filterDto.SortBy?.ToLower().Trim();
            
            if (string.IsNullOrEmpty(sortBy) || sortBy == "id")
            {
                if (filterDto.SortDescending)
                {
                    query = query.OrderByDescending(u => u.Id);
                }
                else
                {
                    query = query.OrderBy(u => u.Id);
                }
            }
            else if (sortBy == "fullname")
            {
                if (filterDto.SortDescending)
                {
                    query = query.OrderByDescending(u => u.FullName);
                }
                else
                {
                    query = query.OrderBy(u => u.FullName);
                }
            }
            else if (sortBy == "registeredat")
            {
                if (filterDto.SortDescending)
                {
                    query = query.OrderByDescending(u => u.RegisteredAt);
                }
                else
                {
                    query = query.OrderBy(u => u.RegisteredAt);
                }
            }
            else
            {
                if (filterDto.SortDescending)
                {
                    query = query.OrderByDescending(u => u.Id);
                }
                else
                {
                    query = query.OrderBy(u => u.Id);
                }
            }

            int totalCount = await query.CountAsync();

            List<User> users = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            List<UserReadDto> dtos = new List<UserReadDto>();

            foreach (User user in users)
            {
                UserReadDto dto = new UserReadDto
                {
                    Id = user.Id,
                    FullName = user.FullName,
                    PhoneNumber = user.PhoneNumber,
                    Role = user.Role,
                    RegisteredAt = user.RegisteredAt.ToUniversalTime()
                };
                dtos.Add(dto);
            }

            return new PagedList<UserReadDto>(dtos, totalCount, pageNumber, pageSize);
        }

        public async Task<UserReadDto> CreateAdminAsync(UserCreateDto dto)
        {
            User user = new User
            {
                FullName = dto.FullName,
                PhoneNumber = dto.PhoneNumber,
                Password = dto.Password,
                RegisteredAt = DateTime.UtcNow.ToUniversalTime(),
                Role = "Admin"
            };

            await unitOfWork.Users.AddAsync(user);
            await unitOfWork.SaveChangesAsync();

            UserReadDto result = new UserReadDto
            {
                Id = user.Id,
                FullName = user.FullName,
                PhoneNumber = user.PhoneNumber,
                Role = user.Role,
                RegisteredAt = user.RegisteredAt.ToUniversalTime()
            };
            return result;
        }
    }
}
