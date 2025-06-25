using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Configurations;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.DB
{
    public class LibraCoreDbContext : DbContext
    {
        public LibraCoreDbContext(DbContextOptions<LibraCoreDbContext> options): base(options)
        {

        }

        public DbSet<User> Users { get; set; } 
        public DbSet<Book> Books { get; set; } 
        public DbSet<Author> Authors { get; set; }
        public DbSet<Genre> Genres { get; set; } 
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Review> Reviews { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AuthorConfiguration());
            modelBuilder.ApplyConfiguration(new BookConfiguration());
            modelBuilder.ApplyConfiguration(new GenreConfiguration());
            modelBuilder.ApplyConfiguration(new ReservationConfiguration());
            modelBuilder.ApplyConfiguration(new ReviewConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
