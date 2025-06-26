using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;

namespace DAL.Generic_Repository.Interfaces
{
    public interface IAuthorRepository : IGenericRepository<Author>
    {
        Task<Dictionary<string, int>> GetAuthorsWithBooksCountAsync();
        Task<List<Author>> GetPagedAuthorsAsync(int pageNumber, int pageSize);
        Task<int> GetTotalAuthorsCountAsync();
        IQueryable<Author> GetQueryable();
    }
}
