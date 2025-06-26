using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DTO.Author;
using FluentValidation;

namespace BLL.Validators.Author
{
    public class AuthorUpdateDtoValidator : AbstractValidator<AuthorUpdateDto>
    {
        public AuthorUpdateDtoValidator() 
        {
            RuleFor(x => x.FullName)
                .NotEmpty()
                .WithMessage("Full name is required.")
                .MaximumLength(200)
                .WithMessage("Full name lenght must be less than 200 symbols");

            RuleFor(x => x.BirthDate)
                .LessThan(DateTime.Now).WithMessage("Birth date must be in the past.");

            RuleFor(x => x.Country)
                .NotEmpty()
                .WithMessage("Country is required.")
                .MaximumLength(100)
                .WithMessage("Country lenght must be less than 100 symbols");

            RuleFor(x => x.Biography)
                .NotEmpty()
                .WithMessage("Biography of author is required!")
                .MaximumLength(1000)
                .WithMessage("Biography cannot exceed 1000 characters.");
        }
    }
}
