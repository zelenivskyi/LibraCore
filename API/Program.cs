using System.Linq;
using BLL.Interfaces;
using BLL.Services;
using DAL.DB;
using DAL.Entities;
using DAL.SeedData;
using DAL.UOW;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<LibraCoreDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<IAuthorService, AuthorService>();
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IGenreService, GenreService>();
builder.Services.AddScoped<IReservationService, ReservationService>();
builder.Services.AddScoped<IReviewService, ReviewService>();
builder.Services.AddScoped<IUserService, UserService>();

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    List<Author> authors = new List<Author>();
    List<Genre> genres = new List<Genre>();
    List<Book> books = new List<Book>();    
    List<User> users = new List<User>();
    List<Review> reviews = new List<Review>();
    List<Reservation> reservations = new List<Reservation>();

    LibraCoreDbContext dbContext = scope.ServiceProvider.GetRequiredService<LibraCoreDbContext>();
    if (!dbContext.Authors.Any())
    {
        authors = SeedAuthor.SeedData(dbContext);
    }
    else
    {
        authors = dbContext.Authors.ToList();
    }

    if (!dbContext.Genres.Any())
    {
        genres = SeedGenre.SeedData(dbContext);
    }
    else
    {
        genres = dbContext.Genres.ToList();
    }

    if (!dbContext.Books.Any())
    {
        books = SeedBook.SeedData(dbContext, genres, authors);
    }
    else
    {
        books = dbContext.Books.ToList();
    }

    if (!dbContext.Users.Any())
    {
        users = SeedUser.SeedData(dbContext);
    }
    else
    {
        users = dbContext.Users.ToList();
    }

    if (!dbContext.Reviews.Any())
    {
        reviews = SeedReview.SeedData(dbContext, users, books);
    }

    if (!dbContext.Reservations.Any())
    {
        reservations = SeedReservation.SeedData(dbContext, users, books);
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
