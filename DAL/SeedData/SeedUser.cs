using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Bogus;
using DAL.DB;
using DAL.Entities;

namespace DAL.SeedData
{
    public class SeedUser
    {
        public static List<User> SeedData(LibraCoreDbContext context)
        {
            List<User> result = new List<User>();
            Faker faker = new Faker("en");

            for (int i = 0; i < 45; i++)
            {
                User user = new User();
                user.FullName = faker.Name.FullName(); 
                user.PhoneNumber = faker.Phone.PhoneNumber();
                user.Password = faker.Internet.Password();
                user.Role = faker.PickRandom(new[] { "Librarian", "Admin" });
                user.RegisteredAt = faker.Date.Past(2).ToUniversalTime();
                result.Add(user);
            }
            context.AddRange(result);
            context.SaveChanges();
            
            return result;
        }
    }
}
