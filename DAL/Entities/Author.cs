﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Author
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Country { get; set; }
        public string Biography { get; set; }
        public ICollection<Book> Books { get; set; } = new List<Book>();
    }
}
