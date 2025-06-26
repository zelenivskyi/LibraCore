using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Filters;
using FluentValidation;

namespace BLL.Validators.Filters
{
    public class UserFilterDtoValidator : AbstractValidator<UserFilterDto>
    {
        public UserFilterDtoValidator()
        {
            RuleFor(x => x.Role)
                .Must(role =>string.IsNullOrEmpty(role) || role.ToLower() == "admin" || role.ToLower() == "librarian")
                .WithMessage("Role must be either 'admin' or 'librarian' if specified");

            RuleFor(x => x.SortBy)
                .Must(sort => string.IsNullOrEmpty(sort) || sort.ToLower() == "id" || sort.ToLower() == "fullname" || sort.ToLower() == "registeredat")
                .WithMessage("SortBy must be one of: 'id', 'fullname', or 'registeredat'");
        }
    }
}
