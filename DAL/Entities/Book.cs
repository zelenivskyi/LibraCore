using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int PublishedYear { get; set; }
        public int Pages { get; set; }
        public string Photo { get; set; }

        public int GenreId { get; set; }
        public Genre Genre { get; set; } 

        public int AuthorId { get; set; }
        public Author Author { get; set; }

        public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
    }
}
