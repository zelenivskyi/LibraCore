using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Filters;
using FluentValidation;

namespace BLL.Validators.Filters
{
    public class AuthorFilterDtoValidator : AbstractValidator<AuthorFilterDto>
    {
        public AuthorFilterDtoValidator() 
        {
            RuleFor(x => x.SortBy)
                .Must(value => string.IsNullOrEmpty(value) ||
                               new[] { "fullname", "country", "id" }.Contains(value.ToLower()))
                .WithMessage("SortBy must be either 'fullname', 'country', or 'id' if specified");
        }
    }
}
