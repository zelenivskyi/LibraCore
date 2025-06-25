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
    public class SeedReview
    {
        public static List<Review> SeedData(LibraCoreDbContext context, List<User> users, List<Book> books)
        {
            List<Review> reviews = new List<Review>();
            Faker faker = new Faker("en");
            HashSet<string> usedPairs = new HashSet<string>();

            for (int i = 0; i < 100; i++)
            {
                User user = faker.PickRandom(users);
                Book book = faker.PickRandom(books);
                string key = $"{user.Id}_{book.Id}";

                if (usedPairs.Contains(key))
                {
                    i--; 
                    continue;
                }
                usedPairs.Add(key);

                Review review = new Review();
                review.UserId = user.Id;
                review.BookId = book.Id;
                review.Rating = faker.Random.Int(1, 5);
                review.Comment = faker.Lorem.Sentence();
                review.CreatedAt = faker.Date.Past(2).ToUniversalTime();
                reviews.Add(review);
            }

            context.Reviews.AddRange(reviews);
            context.SaveChanges();
            return reviews;
        }
    }
}
