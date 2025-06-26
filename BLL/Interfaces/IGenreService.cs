using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DTO.Genre;

namespace BLL.Interfaces
{
    public interface IGenreService
    {
        Task<List<GenreReadDto>> GetAllAsync();
        Task<GenreReadDto> GetByIdAsync(int id);
        Task<GenreReadDto> CreateAsync(GenreCreateDto dto);
        Task<GenreReadDto> UpdateAsync(int id, GenreUpdateDto dto);
        Task<bool> DeleteAsync(int id);
        Task<Dictionary<string, int>> GetGenresWithBooksCountAsync();
    }
}
