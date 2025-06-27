using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DTO.Genre;
using BLL.Interfaces;
using DAL.Entities;
using DAL.UOW;

namespace BLL.Services
{
    public class GenreService : IGenreService
    {
        private IUnitOfWork unitOfWork;

        public GenreService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<List<GenreReadDto>> GetAllAsync()
        {
            List<Genre> genres = await unitOfWork.Genres.GetAllAsync();
            List<GenreReadDto> result = new List<GenreReadDto>();
            
            foreach(Genre genre in genres)
            {
                GenreReadDto readDto = new GenreReadDto
                {
                    Id = genre.Id,
                    Name = genre.Name
                };
                result.Add(readDto);
            }

            return result;
        }

        public async Task<GenreReadDto> GetByIdAsync(int id)
        {
            Genre genre = await unitOfWork.Genres.GetByIdAsync(id);
            if (genre == null)
            {
                return null;
            }

            GenreReadDto result = new GenreReadDto
            {
                Id = genre.Id,
                Name = genre.Name
            };
            return result;
        }

        public async Task<GenreReadDto> CreateAsync(GenreCreateDto dto)
        {
            if (await unitOfWork.Genres.NameExistsAsync(dto.Name))
            {
                throw new Exception("Genre with the same name already exists.");
            }

            Genre genre = new Genre
            {
                Name = dto.Name
            };

            await unitOfWork.Genres.AddAsync(genre);
            await unitOfWork.SaveChangesAsync();

            GenreReadDto result = new GenreReadDto
            {
                Id = genre.Id,
                Name = genre.Name
            };
            return result;
        }

        public async Task<GenreReadDto> UpdateAsync(int id, GenreUpdateDto dto)
        {
            Genre? existingGenre = await unitOfWork.Genres.GetByIdAsync(id);
            if (existingGenre == null)
            {
                return null;
            }

            if (await unitOfWork.Genres.NameExistsAsync(dto.Name, id))
            {
                throw new Exception("Another genre with the same name already exists.");
            }

            existingGenre.Name = dto.Name;
            unitOfWork.Genres.Update(existingGenre);
            await unitOfWork.SaveChangesAsync();

            GenreReadDto result = new GenreReadDto
            {
                Id = existingGenre.Id,
                Name = existingGenre.Name
            };
            return result;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            Genre genre = await unitOfWork.Genres.GetByIdAsync(id);
            if (genre == null)
            {
                return false;
            }

            unitOfWork.Genres.Delete(genre);
            await unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<Dictionary<string, int>> GetGenresWithBooksCountAsync()
        {
            Dictionary<string, int> result = await unitOfWork.Genres.GetGenresWithBooksCountAsync();
            return result;
        }
    }
}
