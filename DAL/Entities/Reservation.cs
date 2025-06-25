using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Reservation
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
        public int BookId { get; set; }
        public Book Book { get; set; }
        public DateTime ReservedAt { get; set; }
        public DateTime? ReturnedAt { get; set; }
        public string Status { get; set; }
    }
}
