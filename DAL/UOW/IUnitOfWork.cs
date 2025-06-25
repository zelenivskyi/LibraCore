using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Generic_Repository.Interfaces;

namespace DAL.UOW
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository Users { get; }
        IBookRepository Books { get; }
        IAuthorRepository Authors { get; }
        IGenreRepository Genres { get; }
        IReservationRepository Reservations { get; }
        IReviewRepository Reviews { get; }
        Task<int> SaveChangesAsync();
    }
}
