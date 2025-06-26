using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DTO.Author;
using BLL.DTO.Book;
using BLL.Filters;
using BLL.Paginate;

namespace BLL.Interfaces
{
    public interface IBookService
    {
        Task<BookReadDto> GetByIdAsync(int id);
        Task<List<BookReadDto>> GetAllAsync();
        Task<List<BookReadDto>> GetBooksByAuthorIdAsync(int authorId);
        Task<List<BookReadDto>> GetBooksByGenreIdAsync(int genreId);
        Task<List<BookReadDto>> GetLatestBooksAsync(int count);
        Task<PagedList<BookReadDto>> GetPagedBooksAsync(int pageNumber, int pageSize);
        Task<BookReadDto> CreateAsync(BookCreateDto dto);
        Task<PagedList<BookReadDto>> GetFilteredBooksAsync(BookFilterDto filterDto, int pageNumber, int pageSize);
        Task UpdateAsync(int id, BookUpdateDto dto);
        Task DeleteAsync(int id);
    }
}
