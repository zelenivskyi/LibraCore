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
                .Where(book => book.AuthorId == authorId)
                .Include(book => book.Author)
                .Include(book => book.Genre)
                .ToListAsync();
        }

        public async Task<List<Book>> GetBooksByGenreIdAsync(int genreId)
        {
            return await dbSet
                .Where(book => book.GenreId == genreId)
                .Include(book => book.Author)
                .Include(book => book.Genre)
                .ToListAsync();
        }

        public async Task<List<Book>> GetLatestBooksAsync(int count)
        {
            return await dbSet
                .OrderByDescending(book => book.PublishedDate)
                .Include(book => book.Author)
                .Include(book => book.Genre)
                .Take(count)
                .ToListAsync();
        }

        public async Task<List<Book>> GetPagedBooksAsync(int pageNumber, int pageSize)
        {
            return await dbSet
                .OrderBy(book => book.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Include(book => book.Author)
                .Include(book => book.Genre)
                .ToListAsync();
        }

        public async Task<int> GetTotalBooksCountAsync()
        {
            return await dbSet.CountAsync();
        }

        public async Task<List<Book>> GetAllWithDetailsAsync()
        {
            return await dbSet
                .Include(book => book.Author)
                .Include(book => book.Genre)
                .Include(book => book.Reservations)
                .Include(book => book.Reviews)
                .ToListAsync();
        }
    }
}
