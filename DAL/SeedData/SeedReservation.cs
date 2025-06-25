using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Bogus;
using DAL.DB;
using DAL.Entities;

namespace DAL.SeedData
{
    public class SeedReservation
    {
        public static List<Reservation> SeedData(LibraCoreDbContext context, List<User> users, List<Book> books)
        {
            List<Reservation> result = new List<Reservation>();
            Faker faker = new Faker("en"); 

            for(int i = 0; i < 50; i++)
            {
                Reservation reservation = new Reservation();
                reservation.UserId = faker.PickRandom(users).Id;
                reservation.BookId = faker.PickRandom(books).Id;
                reservation.ReservedAt = faker.Date.Past(2).ToUniversalTime();

                bool returned = faker.Random.Bool();
                if (returned)
                {
                    reservation.ReturnedAt = faker.Date.Past(1).ToUniversalTime();
                    reservation.Status = "Returned";
                }
                else
                {
                    reservation.Status = "Reserved";
                }
                result.Add(reservation);
            }
            context.AddRange(result);
            context.SaveChanges();

            return result;
        }
    }
}
