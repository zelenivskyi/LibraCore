using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Filters
{
    public class AuthorFilterDto
    {
        public string? FullName { get; set; } 
        public string? Country { get; set; }
        public string? SortBy { get; set; } = "id";
        public bool SortDescending { get; set; } = false;  
    }
}
