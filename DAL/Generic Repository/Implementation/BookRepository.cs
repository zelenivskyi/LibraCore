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
    public class BookRepository : GenericRepository<Book>, IBookRepository
    {
        public BookRepository(DbContext context) : base(context)
        {

        }
        public async Task<List<Book>> GetBooksByAuthorIdAsync(int authorId)
        {
            return await dbSet
                .Where(b => b.AuthorId == authorId)
                .Include(b => b.Author)
                .Include(b => b.Genre)
                .ToListAsync();
        }

        public async Task<List<Book>> GetBooksByGenreIdAsync(int genreId)
        {
            return await dbSet
                .Where(b => b.GenreId == genreId)
                .Include(b => b.Author)
                .Include(b => b.Genre)
                .ToListAsync();
        }

        public async Task<List<Book>> GetLatestBooksAsync(int count)
        {
            return await dbSet
                .OrderByDescending(b => b.PublishedDate)
                .Include(b => b.Author)
                .Include(b => b.Genre)
                .Take(count)
                .ToListAsync();
        }

        public async Task<List<Book>> GetPagedBooksAsync(int pageNumber, int pageSize)
        {
            return await dbSet
                .OrderBy(b => b.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Include(b => b.Author)
                .Include(b => b.Genre)
            .ToListAsync();
        }

        public async Task<int> GetTotalBooksCountAsync()
        {
            return await dbSet.CountAsync();
        }

        public async Task<List<Book>> GetAllWithDetailsAsync()
        {
            return await dbSet
                .Include(b => b.Author)
                .Include(b => b.Genre)
                .Include(b => b.Reservations)
                .Include(b => b.Reviews)
                .ToListAsync();
        }
    }
}
