using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DTO.User;
using FluentValidation;

namespace BLL.Validators.User
{
    public class UserUpdateDtoValidator : AbstractValidator<UserUpdateDto>
    {
        public UserUpdateDtoValidator() 
        {
            RuleFor(p => p.FullName)
                .NotEmpty()
                .WithMessage("Full name is required and cannot be empty")
                .MaximumLength(200)
                .WithMessage("Full name lenght cannot be bigger than 200 symbols");

            RuleFor(p => p.PhoneNumber)
                .NotEmpty()
                .WithMessage("Phone number is required and cannot be empty")
                .MaximumLength(100)
                .WithMessage("Phone number lenght cannot be bigger than 100 symbols");

            RuleFor(p => p.Password)
                .NotEmpty()
                .WithMessage("Password is required and cannot be empty")
                .MaximumLength(500)
                .WithMessage("Password lenght cannot be bigger than 500 symbols");
        }
    }
}
