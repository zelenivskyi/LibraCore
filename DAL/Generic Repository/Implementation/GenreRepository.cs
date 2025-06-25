using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;
using DAL.Generic_Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Generic_Repository.Implementation
{
    public class GenreRepository : GenericRepository<Genre>, IGenreRepository
    {
        public GenreRepository(DbContext context) : base(context)
        {

        }

        public async Task<Dictionary<string, int>> GetGenresWithBooksCountAsync()
        {
            return await dbSet
                .Select(genre => new { genre.Name, BooksCount = genre.Books.Count })
                .ToDictionaryAsync(genre => genre.Name, genre => genre.BooksCount);
        }
    }

}
