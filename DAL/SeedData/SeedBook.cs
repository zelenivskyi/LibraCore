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
    public class SeedBook
    {
        public static List<Book> SeedData(LibraCoreDbContext context, List<Genre> genres, List<Author> authors)
        {
            List<Book> result = new List<Book>();
            Faker faker = new Faker("en");
            for (int i = 0; i < 40; i++)
            {
                Book book = new Book();
                book.Title = faker.Company.CompanyName();
                book.Description = faker.Lorem.Paragraph();
                book.PublishedDate = faker.Date.Past(5).ToUniversalTime();
                book.Pages = faker.Random.Int(100, 1000);
                book.Photo = faker.Internet.Url();
                book.GenreId = faker.PickRandom(genres).Id;
                book.AuthorId = faker.PickRandom(authors).Id;
                result.Add(book);
            }

            context.Books.AddRange(result);
            context.SaveChanges();
            return result;
        }
    }
}
