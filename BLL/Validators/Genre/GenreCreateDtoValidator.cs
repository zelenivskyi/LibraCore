using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DTO.Genre;
using FluentValidation;

namespace BLL.Validators.Genre
{
    public class GenreCreateDtoValidator : AbstractValidator<GenreCreateDto>
    {
        public GenreCreateDtoValidator() 
        {
            RuleFor(p => p.Name)
                .NotEmpty()
                .WithMessage("Genre name is required and cannot be empty")
                .MaximumLength(100)
                .WithMessage("Genre name must be less than 100 symbols");
        }
    }
}
