using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DTO.Author;
using BLL.Interfaces;
using DAL.Entities;
using DAL.UOW;

namespace BLL.Services
{
    public class AuthorService : IAuthorService
    {
        private IUnitOfWork unitOfWork;

        public AuthorService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<AuthorReadDto> GetByIdAsync(int id)
        {
            Author? author = await unitOfWork.Authors.GetByIdAsync(id);
            if (author == null)
            {
                return null;
            }

            AuthorReadDto result = new AuthorReadDto
            {
                Id = author.Id,
                FullName = author.FullName,
                BirthDate = author.BirthDate.ToUniversalTime(),
                Country = author.Country,
                Biography = author.Biography
            };
            return result;
        }

        public async Task<List<AuthorReadDto>> GetAllAsync()
        {
            List<Author> allAuthors = await unitOfWork.Authors.GetAllAsync();
            List<AuthorReadDto> list = new List<AuthorReadDto>();

            foreach (Author author in allAuthors)
            {
                AuthorReadDto newAuthor = new AuthorReadDto
                {
                    Id = author.Id,
                    FullName = author.FullName,
                    BirthDate = author.BirthDate.ToUniversalTime(),
                    Country = author.Country,
                    Biography = author.Biography
                };
                list.Add(newAuthor);
            }
            return list;
        }

        public Task<List<AuthorReadDto>> GetPagedAuthorsAsync(int pageNumber, int pageSize)
        {
            throw new NotImplementedException();
        }

        private async Task<int> GetTotalAuthorsCountAsync()
        {
            return await unitOfWork.Authors.GetTotalAuthorsCountAsync();
        }

        public async Task<Dictionary<string, int>> GetAuthorsWithBooksCountAsync()
        {
            return await unitOfWork.Authors.GetAuthorsWithBooksCountAsync();
        }

        public async Task<AuthorReadDto> CreateAsync(AuthorCreateDto dto)
        {
            Author author = new Author
            {
                FullName = dto.FullName,
                BirthDate = dto.BirthDate.ToUniversalTime(),
                Country = dto.Country,
                Biography = dto.Biography
            };

            await unitOfWork.Authors.AddAsync(author);
            await unitOfWork.SaveChangesAsync();
            
            AuthorReadDto result = new AuthorReadDto
            {
                Id = author.Id,
                FullName = author.FullName,
                BirthDate = author.BirthDate.ToUniversalTime(),
                Country = author.Country,
                Biography = author.Biography
            };
            return result;
        }

        public async Task UpdateAsync(int id, AuthorUpdateDto dto)
        {
            Author? author = await unitOfWork.Authors.GetByIdAsync(id);
            if (author == null)
            {
                throw new Exception("Author not found");
            }

            author.FullName = dto.FullName;
            author.BirthDate = dto.BirthDate.ToUniversalTime();
            author.Country = dto.Country;
            author.Biography = dto.Biography;

            unitOfWork.Authors.Update(author);
            await unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            Author? author = await unitOfWork.Authors.GetByIdAsync(id);
            if (author == null)
            {
                throw new Exception("Author not found");
            }

            unitOfWork.Authors.Delete(author);
            await unitOfWork.SaveChangesAsync();
        }
    }
}
