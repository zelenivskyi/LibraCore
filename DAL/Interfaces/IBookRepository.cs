using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;

namespace DAL.Generic_Repository.Interfaces
{
    public interface IBookRepository : IGenericRepository<Book>
    {
        Task<bool> TitleAndAuthorExistsAsync(string title, int authorId, int? excludeBookId = null);
        Task<Book> GetBookByIdWithDetailsAsync(int id);
        Task<List<Book>> GetLatestBooksAsync(int count);
        Task<List<Book>> GetPagedBooksAsync(int pageNumber, int pageSize);
        Task<int> GetTotalBooksCountAsync();
        IQueryable<Book> GetQueryable();
        Task<List<Book>> GetAllWithDetailsAsync();
    }
}
