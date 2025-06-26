using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTO.Reservation
{
    public class ReservationReadDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string UserFullName { get; set; } 
        public int BookId { get; set; }
        public string BookTitle { get; set; } 
        public DateTime ReservedAt { get; set; }
        public DateTime? ReturnedAt { get; set; }
        public string Status { get; set; }
    }
}
