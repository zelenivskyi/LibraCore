using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DTO.Review;
using FluentValidation;
using FluentValidation.Validators;

namespace BLL.Validators.Review
{
    public class ReviewUpdateDtoValidator : AbstractValidator<ReviewUpdateDto>
    {
        public ReviewUpdateDtoValidator() 
        {
            RuleFor(p => p.Rating)
                .GreaterThanOrEqualTo(1)
                .WithMessage("Rating cannot be less than 1 ⭐")
                .LessThanOrEqualTo(5)
                .WithMessage("Rating cannot be bigger than 5 ⭐");

            RuleFor(p => p.Comment)
                .NotEmpty()
                .WithMessage("Comment is required and cannot be empty")
                .MaximumLength(2000)
                .WithMessage("Comment lenght cannot be bigger than 2000 symbols");
        }
    }
}
