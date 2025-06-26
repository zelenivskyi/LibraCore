using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTO.Book
{
    public class BookReadDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime PublishedDate { get; set; }
        public int Pages { get; set; }
        public string Photo { get; set; }
        public int GenreId { get; set; }
        public string GenreName { get; set; } 
        public int AuthorId { get; set; }
        public string AuthorName { get; set; }
    }
}
