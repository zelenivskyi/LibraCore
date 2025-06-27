using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;
using DAL.Generic_Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Implementation
{
    public class GenreRepository : GenericRepository<Genre>, IGenreRepository
    {
        public GenreRepository(DbContext context) : base(context)
        {

        }

        public async Task<bool> NameExistsAsync(string name, int? excludeGenreId = null)
        {
            var query = dbSet
                .Where(g => g.Name.ToLower() == name.ToLower());

            if (excludeGenreId.HasValue)
            {
                query = query.Where(g => g.Id != excludeGenreId.Value);
            }

            return await query.AnyAsync();
        }

        public async Task<Dictionary<string, int>> GetGenresWithBooksCountAsync()
        {
            return await dbSet
                .Select(genre => new { genre.Name, BooksCount = genre.Books.Count })
                .ToDictionaryAsync(genre => genre.Name, genre => genre.BooksCount);
        }
    }

}
