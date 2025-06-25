using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.DB;
using DAL.Entities;

namespace DAL.SeedData
{
    public class SeedGenre
    {
        public static List<Genre> SeedData(LibraCoreDbContext context)
        {
            List<Genre> result = new List<Genre>();
            List<string> genresType = new List<string>() { 
                "Fantasy", "Mystery", "Romance",
                "Science Fiction", "Thriller", "Adventure",
                "Horror", "Historical", "Biography", "Self-help"
            };
            foreach (string genre in genresType)
            {
                Genre newGenre = new Genre();
                newGenre.Name = genre;
                result.Add(newGenre);
            }
            context.AddRange(result);
            context.SaveChanges(); 

            return result;
        } 
    }
}