using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Filters
{
    public class ReservationFilterDto
    {
        public string? Status { get; set; }
        public int? UserId { get; set; }
        public int? BookId { get; set; }
        public DateTime? ReservedFrom { get; set; }
        public DateTime? ReservedTo { get; set; }
        public string SortBy { get; set; } = "id";  
        public bool SortDescending { get; set; } = false;
    }
}
