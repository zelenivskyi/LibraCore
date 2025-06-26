using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DTO.Author;
using BLL.Filters;
using BLL.Paginate;

namespace BLL.Interfaces
{
    public interface IAuthorService
    {
        Task<PagedList<AuthorReadDto>> GetFilteredAuthorsAsync(AuthorFilterDto filterDto, int pageNumber, int pageSize);
        Task<AuthorReadDto> GetByIdAsync(int id);
        Task<List<AuthorReadDto>> GetAllAsync();
        Task<PagedList<AuthorReadDto>> GetPagedAuthorsAsync(int pageNumber, int pageSize);
        Task<Dictionary<string, int>> GetAuthorsWithBooksCountAsync();
        Task<AuthorReadDto> CreateAsync(AuthorCreateDto dto);
        Task UpdateAsync(int id, AuthorUpdateDto dto);
        Task DeleteAsync(int id);
    }
}
