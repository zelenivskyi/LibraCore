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
    public class AuthorRepository : GenericRepository<Author>, IAuthorRepository
    {
        public AuthorRepository(DbContext context) : base(context)
        {

        }

        public IQueryable<Author> GetQueryable()
        {
            return dbSet.AsQueryable();
        }

        public async Task<Dictionary<string, int>> GetAuthorsWithBooksCountAsync()
        {
            return await dbSet
                .Select(author => new { author.FullName, BooksCount = author.Books.Count })
                .ToDictionaryAsync(author => author.FullName, author => author.BooksCount);
        }

        public async Task<List<Author>> GetPagedAuthorsAsync(int pageNumber, int pageSize)
        {
            return await dbSet
                .OrderBy(author => author.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<int> GetTotalAuthorsCountAsync()
        {
            return await dbSet.CountAsync();
        }
    }
}
