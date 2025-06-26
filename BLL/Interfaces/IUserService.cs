using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DTO.User;
using BLL.Filters;
using BLL.Paginate;

namespace BLL.Interfaces
{
    public interface IUserService
    {
        Task<PagedList<UserReadDto>> GetFilteredUsersAsync(UserFilterDto filterDto, int pageNumber, int pageSize);
        Task<PagedList<UserReadDto>> GetPagedUsersAsync(int pageNumber, int pageSize);
        Task<List<UserReadDto>> GetAllAsync();
        Task<UserReadDto> GetByIdAsync(int id);
        Task<UserReadDto> CreateLibrarianAsync(UserCreateDto dto);
        Task<UserReadDto> CreateAdminAsync(UserCreateDto dto);
        Task<UserReadDto> UpdateAsync(int id, UserUpdateDto dto);
        Task<bool> DeleteAsync(int id);
    }
}