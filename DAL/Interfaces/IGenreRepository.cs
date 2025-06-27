using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;

namespace DAL.Generic_Repository.Interfaces
{
    public interface IGenreRepository : IGenericRepository<Genre>
    {
        Task<bool> NameExistsAsync(string name, int? excludeGenreId = null);
        Task<Dictionary<string, int>> GetGenresWithBooksCountAsync();
    }
}
