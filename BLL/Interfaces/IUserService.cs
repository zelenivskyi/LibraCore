using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DTO.User;

namespace BLL.Interfaces
{
    public interface IUserService
    {
        Task<List<UserReadDto>> GetAllAsync();
        Task<UserReadDto> GetByIdAsync(int id);
        Task<UserReadDto> CreateLibrarianAsync(UserCreateDto dto);
        Task<UserReadDto> CreateAdminAsync(UserCreateDto dto);
        Task<UserReadDto> UpdateAsync(int id, UserUpdateDto dto);
        Task<bool> DeleteAsync(int id);
    }
}