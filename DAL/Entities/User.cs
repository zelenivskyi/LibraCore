    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    namespace DAL.Entities
    {
        public class User
        {
            public int Id { get; set; }
            public string FullName { get; set; }
            public string PhoneNumber { get; set; }
            public string Password { get; set; }
            public string Role { get; set; } = "Librarian";
            public DateTime RegisteredAt { get; set; } = DateTime.Now.ToUniversalTime();
            public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
            public ICollection<Review> Reviews { get; set; } = new List<Review>();
        }
    }
