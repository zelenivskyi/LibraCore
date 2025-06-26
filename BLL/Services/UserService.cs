using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DTO.User;
using BLL.Interfaces;
using DAL.Entities;
using DAL.UOW;

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

        private async Task<List<User>> GetPagedUsersAsync(int pageNumber, int pageSize)
        {
            throw new NotImplementedException();
        }

        private async Task<int> GetTotalUsersCountAsync()
        {
            return await unitOfWork.Users.GetTotalUsersCountAsync();
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
