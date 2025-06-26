using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTO.Reservation
{
    public class ReservationUpdateDto
    {
        public DateTime ReturnedAt { get; set; }
        public string Status { get; set; }
    }
}
