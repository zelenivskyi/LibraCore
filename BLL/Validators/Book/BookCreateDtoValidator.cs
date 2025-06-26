using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DTO.Book;
using FluentValidation;

namespace BLL.Validators.Book
{
    public class BookCreateDtoValidator : AbstractValidator<BookCreateDto>
    {
        public BookCreateDtoValidator() 
        {
            RuleFor(p => p.Title)
                .NotEmpty()
                .WithMessage("Title is required!")
                .MaximumLength(300)
                .WithMessage("Title must be less than 300 symbols");

            RuleFor(p => p.Description)
                .NotEmpty()
                .WithMessage("Description is required!")
                .MaximumLength(2000)
                .WithMessage("Description must be less than 2000 symbols");

            RuleFor(p => p.PublishedDate)
                .NotEmpty()
                .WithMessage("Published date is required")
                .LessThan(DateTime.Now.ToUniversalTime())
                .WithMessage("Published date cannot be now!");

            RuleFor(p => p.Pages)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Pages amount cannot be less than 0");

            RuleFor(p => p.Photo)
                .NotEmpty()
                .WithMessage("Photo url is required!")
                .MaximumLength(500)
                .WithMessage("Photo url must be less than 500 symbols");

            RuleFor(p => p.GenreId)
                .GreaterThanOrEqualTo(1)
                .WithMessage("Genre ID cannot be less than 1");

            RuleFor(p => p.AuthorId)
                .GreaterThanOrEqualTo(1)
                .WithMessage("Author ID cannot be less than 1");
        }
    }
}
