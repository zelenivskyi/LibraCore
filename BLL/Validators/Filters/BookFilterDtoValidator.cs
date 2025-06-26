using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Filters;
using FluentValidation;

namespace BLL.Validators.Filters
{
    public class BookFilterDtoValidator : AbstractValidator<BookFilterDto>
    {
        public BookFilterDtoValidator()
        {
            
            RuleFor(x => x.GenreId)
                .GreaterThanOrEqualTo(1)
                .When(x => x.GenreId.HasValue)
                .WithMessage("GenreId must be greater than or equal to 1 if specified");

            RuleFor(x => x.AuthorId)
                .GreaterThanOrEqualTo(1)
                .When(x => x.AuthorId.HasValue)
                .WithMessage("AuthorId must be greater than or equal to 1 if specified");

            RuleFor(x => x.SortBy)
                .Must(value => string.IsNullOrEmpty(value) ||
                               new[] { "title", "publisheddate", "id"}.Contains(value.ToLower()))
                .WithMessage("SortBy must be either 'title', 'year', or 'id' if specified");
        }
    }
}
