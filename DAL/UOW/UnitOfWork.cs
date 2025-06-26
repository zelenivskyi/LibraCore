using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.DB;
using DAL.Generic_Repository.Interfaces;
using DAL.Implementation;

namespace DAL.UOW
{
    public class UnitOfWork : IUnitOfWork
    {
        private LibraCoreDbContext context;

        public IUserRepository Users { get; }
        public IBookRepository Books { get; }
        public IAuthorRepository Authors { get; }
        public IGenreRepository Genres { get; }
        public IReservationRepository Reservations { get; }
        public IReviewRepository Reviews { get; }

        public UnitOfWork(LibraCoreDbContext context)
        {
            this.context = context;

            Users = new UserRepository(this.context);
            Books = new BookRepository(this.context);
            Authors = new AuthorRepository(this.context);
            Genres = new GenreRepository(this.context);
            Reservations = new ReservationRepository(this.context);
            Reviews = new ReviewRepository(this.context);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await context.SaveChangesAsync();
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}
