using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTO.Author
{
    public class AuthorCreateDto
    {
        public string FullName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Country { get; set; }
        public string Biography { get; set; }
    }
}
