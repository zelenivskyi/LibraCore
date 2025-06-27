using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DTO.Book;
using BLL.Filters;
using BLL.Interfaces;
using BLL.Paginate;
using DAL.Entities;
using DAL.UOW;
using Microsoft.EntityFrameworkCore;

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
            if (await unitOfWork.Books.TitleAndAuthorExistsAsync(dto.Title, dto.AuthorId))
            {
                throw new Exception("Book with the same title and author already exists.");
            }

            Author? authorExists = await unitOfWork.Authors.GetByIdAsync(dto.AuthorId);
            if (authorExists == null)
            {
                throw new Exception($"Author with id {dto.AuthorId} does not exist.");
            }

            Genre? genreExists = await unitOfWork.Genres.GetByIdAsync(dto.GenreId);
            if (genreExists == null)
            {
                throw new Exception($"Genre with id {dto.GenreId} does not exist.");
            }

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
            book = await unitOfWork.Books.GetBookByIdWithDetailsAsync(book.Id);

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

            if (await unitOfWork.Books.TitleAndAuthorExistsAsync(dto.Title, dto.AuthorId, id))
            {
                throw new Exception("Another book with the same title and author already exists.");
            }

            Author? authorExists = await unitOfWork.Authors.GetByIdAsync(dto.AuthorId);
            if (authorExists == null)
            {
                throw new Exception($"Author with id {dto.AuthorId} does not exist.");
            }

            Genre? genreExists = await unitOfWork.Genres.GetByIdAsync(dto.GenreId);
            if (genreExists == null)
            {
                throw new Exception($"Genre with id {dto.GenreId} does not exist.");
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

        public async Task<PagedList<BookReadDto>> GetPagedBooksAsync(int pageNumber, int pageSize)
        {
            List<Book> books = await unitOfWork.Books.GetPagedBooksAsync(pageNumber, pageSize);
            int totalCount = await unitOfWork.Books.GetTotalBooksCountAsync();
            List<BookReadDto> dtos = new List<BookReadDto>();

            foreach (Book book in books)
            {
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
                dtos.Add(bookReadDto);
            }

            return new PagedList<BookReadDto>(dtos, totalCount, pageNumber, pageSize);
        }

        public async Task<PagedList<BookReadDto>> GetFilteredBooksAsync(BookFilterDto filter, int pageNumber, int pageSize)
        {
            IQueryable<Book> query = unitOfWork.Books.GetQueryable();
            if (!string.IsNullOrEmpty(filter.Title))
            {
                string title = filter.Title.ToLower().Trim();
                query = query.Where(b => b.Title.ToLower().Contains(title));
            }
            if (filter.GenreId.HasValue)
            {
                int genreId = filter.GenreId.Value;
                query = query.Where(b => b.GenreId == genreId);
            }
            if (filter.AuthorId.HasValue)
            {
                int authorId = filter.AuthorId.Value;
                query = query.Where(b => b.AuthorId == authorId);
            }

            if (!string.IsNullOrEmpty(filter.SortBy))
            {
                string sortBy = filter.SortBy.ToLower().Trim();

                if (sortBy == "title")
                {
                    if (filter.SortDescending)
                    {
                        query = query.OrderByDescending(b => b.Title.ToLower().Trim());
                    }
                    else
                    {
                        query = query.OrderBy(b => b.Title.ToLower().Trim());
                    }
                }
                else if (sortBy == "publisheddate")
                {
                    if (filter.SortDescending)
                    {
                        query = query.OrderByDescending(b => b.PublishedDate);
                    }
                    else
                    {
                        query = query.OrderBy(b => b.PublishedDate);
                    }
                }
                else
                {
                    if (filter.SortDescending)
                    {
                        query = query.OrderByDescending(b => b.Id);
                    }
                    else
                    {
                        query = query.OrderBy(b => b.Id);
                    }
                }
            }
            else
            {
                query = query.OrderBy(b => b.Id);
            }
            int totalCount = await query.CountAsync();

            List<Book> books = await query
                .Skip((pageNumber - 1) * pageSize)
                .Include(p => p.Genre)
                .Include(p => p.Author)
                .Take(pageSize)
                .ToListAsync();

            List<BookReadDto> dtos = new List<BookReadDto>();

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
                dtos.Add(dto);
            }

            return new PagedList<BookReadDto>(dtos, totalCount, pageNumber, pageSize);
        }
    }
}
