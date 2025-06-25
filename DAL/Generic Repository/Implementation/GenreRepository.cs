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
                .Select(g => new { g.Name, BooksCount = g.Books.Count })
                .ToDictionaryAsync(x => x.Name, x => x.BooksCount);
        }
    }

}
