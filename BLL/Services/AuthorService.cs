using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DTO.Author;
using BLL.Filters;
using BLL.Interfaces;
using BLL.Paginate;
using DAL.Entities;
using DAL.UOW;
using Microsoft.EntityFrameworkCore;

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

        public async Task<PagedList<AuthorReadDto>> GetPagedAuthorsAsync(int pageNumber, int pageSize)
        {
            List<Author> authors = await unitOfWork.Authors.GetPagedAuthorsAsync(pageNumber, pageSize);
            int totalCount = await unitOfWork.Authors.GetTotalAuthorsCountAsync();
            List<AuthorReadDto> dtos = new List<AuthorReadDto>();

            foreach (Author author in authors)
            {
                AuthorReadDto dto = new AuthorReadDto
                {
                    Id = author.Id,
                    FullName = author.FullName,
                    BirthDate = author.BirthDate.ToUniversalTime(),
                    Country = author.Country,
                    Biography = author.Biography
                };
                dtos.Add(dto);
            }

            return new PagedList<AuthorReadDto>(dtos, totalCount, pageNumber, pageSize);
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

        public async Task<PagedList<AuthorReadDto>> GetFilteredAuthorsAsync(AuthorFilterDto filterDto, int pageNumber, int pageSize)
        {
            IQueryable<Author> query = unitOfWork.Authors.GetQueryable();
            if (!string.IsNullOrEmpty(filterDto.FullName))
            {
                string name = filterDto.FullName.ToLower().Trim();
                query = query.Where(a => a.FullName.ToLower().Contains(name));
            }
            if (!string.IsNullOrEmpty(filterDto.Country))
            {
                string country = filterDto.Country.ToLower().Trim();
                query = query.Where(a => a.Country.ToLower().Contains(country));
            }

            if (!string.IsNullOrEmpty(filterDto.SortBy))
            {
                string sortBy = filterDto.SortBy.ToLower().Trim();

                if (sortBy == "fullname")
                {
                    if (filterDto.SortDescending)
                    {
                        query = query.OrderByDescending(a => a.FullName);
                    }
                    else
                    {
                        query = query.OrderBy(a => a.FullName);
                    }
                }
                else if (sortBy == "country")
                {
                    if (filterDto.SortDescending)
                    {
                        query = query.OrderByDescending(a => a.Country);
                    }
                    else
                    {
                        query = query.OrderBy(a => a.Country);
                    }
                }
                else
                {
                    if (filterDto.SortDescending)
                    {
                        query = query.OrderByDescending(a => a.Id);
                    }
                    else
                    {
                        query = query.OrderBy(a => a.Id);
                    }
                }
            }
            else
            {
                query = query.OrderBy(a => a.Id);
            }

            int totalCount = await query.CountAsync();
            List<Author> authors = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            List<AuthorReadDto> result = new List<AuthorReadDto>();
            foreach (Author author in authors)
            {
                AuthorReadDto dto = new AuthorReadDto
                {
                    Id = author.Id,
                    FullName = author.FullName,
                    BirthDate = author.BirthDate.ToUniversalTime(),
                    Country = author.Country,
                    Biography = author.Biography
                };
                result.Add(dto);
            }

            return new PagedList<AuthorReadDto>(result, totalCount, pageNumber, pageSize);
        }
    }
}
