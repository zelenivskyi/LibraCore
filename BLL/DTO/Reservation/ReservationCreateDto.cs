using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTO.Reservation
{
    public class ReservationCreateDto
    {
        public int UserId { get; set; }
        public int BookId { get; set; }     
    }
}
