using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DTO.Book;
using BLL.Interfaces;
using DAL.Entities;
using DAL.UOW;

namespace BLL.Services
{
    public class BookService : IBookService
    {
        private IUnitOfWork unitOfWork;

        public BookService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<BookReadDto> GetByIdAsync(int id)
        {
            Book book = await unitOfWork.Books.GetBookByIdWithDetailsAsync(id);
            if (book == null)
            {
                return null;
            }

            BookReadDto bookReadDto = new BookReadDto
            {
                Id = book.Id,
                Title = book.Title,
                Description = book.Description,
                PublishedDate = book.PublishedDate.ToUniversalTime(),
                Pages = book.Pages,
                Photo = book.Photo,
                GenreId = book.GenreId,
                GenreName = book.Genre.Name,
                AuthorId = book.AuthorId,
                AuthorName = book.Author.FullName
            };

            return bookReadDto;
        }

        public async Task<List<BookReadDto>> GetAllAsync()
        {
            List<Book> books = await unitOfWork.Books.GetAllWithDetailsAsync();
            List<BookReadDto> result = new List<BookReadDto>();
            
            foreach (Book book in books)
            {
                BookReadDto dto = new BookReadDto
                {
                    Id = book.Id,
                    Title = book.Title,
                    Description = book.Description,
                    PublishedDate = book.PublishedDate.ToUniversalTime(),
                    Pages = book.Pages,
                    Photo = book.Photo,
                    GenreId = book.GenreId,
                    GenreName = book.Genre.Name,
                    AuthorId = book.AuthorId,
                    AuthorName = book.Author.FullName
                };

                result.Add(dto);
            }

            return result;
        }

        public async Task<List<BookReadDto>> GetBooksByAuthorIdAsync(int authorId)
        {
            List<Book> books = await unitOfWork.Books.GetBooksByAuthorIdAsync(authorId);
            List<BookReadDto> result = new List<BookReadDto>();
            
            foreach (Book book in books)
            {
                BookReadDto dto = new BookReadDto
                {
                    Id = book.Id,
                    Title = book.Title,
                    Description = book.Description,
                    PublishedDate = book.PublishedDate.ToUniversalTime(),
                    Pages = book.Pages,
                    Photo = book.Photo,
                    GenreId = book.GenreId,
                    GenreName = book.Genre.Name,
                    AuthorId = book.AuthorId,
                    AuthorName = book.Author.FullName
                };

                result.Add(dto);
            }

            return result;
        }

        public async Task<List<BookReadDto>> GetBooksByGenreIdAsync(int genreId)
        {
            List<Book> books = await unitOfWork.Books.GetBooksByGenreIdAsync(genreId);
            List<BookReadDto> result = new List<BookReadDto>();
            
            foreach (Book book in books)
            {
                BookReadDto dto = new BookReadDto
                {
                    Id = book.Id,
                    Title = book.Title,
                    Description = book.Description,
                    PublishedDate = book.PublishedDate.ToUniversalTime(),
                    Pages = book.Pages,
                    Photo = book.Photo,
                    GenreId = book.GenreId,
                    GenreName = book.Genre.Name,
                    AuthorId = book.AuthorId,
                    AuthorName = book.Author.FullName
                };

                result.Add(dto);
            }

            return result;
        }

        public async Task<List<BookReadDto>> GetLatestBooksAsync(int count)
        {
            List<Book> books = await unitOfWork.Books.GetLatestBooksAsync(count);
            List<BookReadDto> result = new List<BookReadDto>();

            foreach (Book book in books)
            {
                BookReadDto dto = new BookReadDto
                {
                    Id = book.Id,
                    Title = book.Title,
                    Description = book.Description,
                    PublishedDate = book.PublishedDate.ToUniversalTime(),
                    Pages = book.Pages,
                    Photo = book.Photo,
                    GenreId = book.GenreId,
                    GenreName = book.Genre.Name,
                    AuthorId = book.AuthorId,
                    AuthorName = book.Author.FullName
                };

                result.Add(dto);
            }

            return result;
        }

        public async Task<BookReadDto> CreateAsync(BookCreateDto dto)
        {
            Book book = new Book
            {
                Title = dto.Title,
                Description = dto.Description,
                PublishedDate = dto.PublishedDate.ToUniversalTime(),
                Pages = dto.Pages,
                Photo = dto.Photo,
                GenreId = dto.GenreId,
                AuthorId = dto.AuthorId
            };

            await unitOfWork.Books.AddAsync(book);
            await unitOfWork.SaveChangesAsync();
            List<Book> books = await unitOfWork.Books.GetAllWithDetailsAsync();
            book = books.Last();

            BookReadDto bookReadDto = new BookReadDto
            {
                Id = book.Id,
                Title = book.Title,
                Description = book.Description,
                PublishedDate = book.PublishedDate.ToUniversalTime(),
                Pages = book.Pages,
                Photo = book.Photo,
                GenreId = book.GenreId,
                GenreName = book.Genre.Name,
                AuthorId = book.AuthorId,
                AuthorName = book.Author.FullName
            };

            return bookReadDto;
        }

        public async Task UpdateAsync(int id, BookUpdateDto dto)
        {
            Book? book = await unitOfWork.Books.GetByIdAsync(id);
            if (book == null)
            {
                throw new Exception("Book not found");
            }

            book.Title = dto.Title;
            book.Description = dto.Description;
            book.PublishedDate = dto.PublishedDate.ToUniversalTime();
            book.Pages = dto.Pages;
            book.Photo = dto.Photo;
            book.GenreId = dto.GenreId;
            book.AuthorId = dto.AuthorId;

            unitOfWork.Books.Update(book);
            await unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            Book? book = await unitOfWork.Books.GetByIdAsync(id);
            
            if (book == null)
            {
                throw new Exception("Book not found");
            }
            unitOfWork.Books.Delete(book);
            await unitOfWork.SaveChangesAsync();
        }

        public Task<List<BookReadDto>> GetPagedBooksAsync(int pageNumber, int pageSize)
        {
            throw new NotImplementedException();
        }

        private async Task<int> GetTotalBooksCountAsync()
        {
            return await unitOfWork.Books.GetTotalBooksCountAsync();
        }
    }
}
