using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bogus;
using DAL.DB;
using DAL.Entities;

namespace DAL.SeedData
{
    public class SeedAuthor
    {
        public static List<Author> SeedData(LibraCoreDbContext context)
        {
            List<Author> result = new List<Author>();
            Faker faker = new Faker("en");
            for(int i = 0; i < 30; i++)
            {
                Author author = new Author();
                author.FullName = faker.Name.FullName();
                author.BirthDate = faker.Date.Past(35).ToUniversalTime();
                author.Country = faker.Address.Country();
                author.Biography = faker.Lorem.Paragraph();
                result.Add(author);
            }

            context.AddRange(result);
            context.SaveChanges();
            return result;
        }
    }
}