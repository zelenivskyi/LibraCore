using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Filters
{
    public class UserFilterDto
    {
        public string? FullName { get; set; } 
        public string? PhoneNumber { get; set; }
        public string? Role { get; set; }
        public string SortBy { get; set; } = "Id";  
        public bool SortDescending { get; set; } = false;
    }
}
